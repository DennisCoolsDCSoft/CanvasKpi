using System.Net;
using CompetenceProfilingDomain.Contracts.Infrastructure;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using Newtonsoft.Json;

namespace CompetenceProfilingInfrastructure.CanvasDao;

public class AssignmentRubricCriteriaRatingDao : IAssignmentRubricCriteriaRatingDao
{
    private readonly HttpClient _httpClient;

    public AssignmentRubricCriteriaRatingDao(ICanvasRestHttpClient canvasRestHttpClient)
    {
        _httpClient = canvasRestHttpClient.HttpClient;
    }
    
    public void CreateAndUpdateRubricSubmissions(int courseId,int studentId,int rubricAssociationId, Dictionary<string,int?> criterionIdAndPoint)
    {
        var dictionary = new Dictionary<string, string>();
        dictionary.Add("rubric_assessment[user_id]", $"{studentId}"); 
        dictionary.Add("rubric_assessment[assessment_type]", "grading");

        foreach (var createPoint in criterionIdAndPoint)
        {
            // string criterionId ,int point,
            if (createPoint.Value != null)
            {
                dictionary.Add($"rubric_assessment[criterion_{createPoint.Key}][points]", $"{createPoint.Value}");
                //Debug.WriteLine($"rubric_assessment[criterion_{createPoint.Key}][points]" + $"{createPoint.Value}");
            }
            else
            {
                dictionary.Add($"rubric_assessment[criterion_{createPoint.Key}][points]", "");
                //Debug.WriteLine($"rubric_assessment[criterion_{createPoint.Key}][points]" + "");
            }
        }


        var formUrlEncodedContent = new FormUrlEncodedContent(dictionary);
        var httpResponseMessage = _httpClient.PostAsync($"/api/v1/courses/{courseId}/rubric_associations/{rubricAssociationId}/rubric_assessments", formUrlEncodedContent).Result;

        if (httpResponseMessage.IsSuccessStatusCode) return;
        var rest = httpResponseMessage.Content.ReadAsStringAsync().Result;
        throw new Exception("UpdateOneRubricSubmission"+rest);
    }
    
    public SubmissionCanvasDto? SubmissionsByCourseAndAssignmentAndUser(int courseId, int assignmentId,int studentId)
    {
        var result = _httpClient
            .GetAsync(
                $@"/api/v1/courses/{courseId}/assignments/{assignmentId}/submissions/{studentId}?include[]=rubric_assessment")
            .Result;

        if (result.StatusCode != HttpStatusCode.OK) return null;
        
        var jsonString = result.Content.ReadAsStringAsync().Result;
        var jsonObject = JsonConvert.DeserializeObject<SubmissionCanvasDto>(jsonString) ?? new SubmissionCanvasDto();
        
        jsonObject.RubricAssessment = ReadRubricAssessmentSubmission(jsonString);
        
        return jsonObject;
    }

    //todo :(
    private static List<RubricAssessmentSubmissionCanvasDto> ReadRubricAssessmentSubmission(string jsonString)
    {
        JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));

        List<RubricAssessmentSubmissionCanvasDto> rest = new List<RubricAssessmentSubmissionCanvasDto>();
        int inToRubricObject = 0;
        string previousValue = "";
        RubricAssessmentSubmissionCanvasDto rubricSub = new RubricAssessmentSubmissionCanvasDto();
        
        while (reader.Read())
        {
            if (previousValue == "rubric_assessment") inToRubricObject++;

            if (inToRubricObject > 0)
            {
                if (reader.TokenType == JsonToken.StartObject)
                    if (previousValue != "rubric_assessment")
                        inToRubricObject++;

                if (reader.TokenType == JsonToken.EndObject) inToRubricObject--;

                if (inToRubricObject > 0)
                {
                    if (reader.Value != null)
                    {
                        if (inToRubricObject == 1)
                        {
                            rubricSub = new RubricAssessmentSubmissionCanvasDto
                            {
                                _id = reader.Value.ToString() ?? ""
                            };
                        }
                        else
                        {
                            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "points")
                            {
                                reader.Read();
                                rubricSub.Points = reader.Value.ToString() ?? "";
                            }
                        }
                        //Console.WriteLine("Token: {0}, Value: {1} num: {2}", reader.TokenType, reader.Value, inToRubricObject);
                    }
                    else
                    {
                        if (inToRubricObject == 1 && reader.TokenType == JsonToken.EndObject)
                        {
                            rest.Add(rubricSub);
                        }
                        //Console.WriteLine("Token: {0} num: {1}", reader.TokenType, inToRubricObject);
                    }
                }
            }

            previousValue = reader.Value?.ToString() ?? "";
        }
        
        return rest;
    }
}

// "_6505": {
//     "rating_id": "blank",
//     "comments": "",
//     "points": 5.0
// },

// Token: PropertyName, Value: rubric_assessment num: 0
// Token: StartObject num: 1
// Token: PropertyName, Value: _6505 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: blank num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: PropertyName, Value: points num: 2
// Token: Float, Value: 5 num: 2
// Token: EndObject num: 1
// Token: PropertyName, Value: _2762 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: _2816 num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: PropertyName, Value: points num: 2
// Token: Float, Value: 4 num: 2
// Token: EndObject num: 1
// Token: PropertyName, Value: _2140 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: _3085 num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: PropertyName, Value: points num: 2
// Token: Float, Value: 3 num: 2
// Token: EndObject num: 1
// Token: PropertyName, Value: _2072 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: _1480 num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: PropertyName, Value: points num: 2
// Token: Float, Value: 0 num: 2
// Token: EndObject num: 1
// Token: PropertyName, Value: _9977 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: _3461 num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: EndObject num: 1
// Token: PropertyName, Value: _818 num: 1
// Token: StartObject num: 2
// Token: PropertyName, Value: rating_id num: 2
// Token: String, Value: _9138 num: 2
// Token: PropertyName, Value: comments num: 2
// Token: String, Value:  num: 2
// Token: EndObject num: 1
// Token: EndObject num: 0