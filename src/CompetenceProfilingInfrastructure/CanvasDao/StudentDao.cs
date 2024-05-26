using System.Net;
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingInfrastructure.CanvasDao.HttpClientExtensions;
using Newtonsoft.Json;

namespace CompetenceProfilingInfrastructure.CanvasDao;

public class StudentDao : IStudentDao
{
    private readonly HttpClient _httpClient;

    public StudentDao(ICanvasRestHttpClient canvasRestHttpClient)
    {
        _httpClient = canvasRestHttpClient.HttpClient;
    }

    public IEnumerable<UserCanvasDto> GetStudentsInCourse(int courseId)
    {
        var command = $@"/api/v1/courses/{courseId}/users?enrollment_role=StudentEnrollment&enrollment_state[]=active&include[]=test_student";
        var result = _httpClient.GetAllPagesAsync(command).Result;
        if (result.StatusCode != HttpStatusCode.OK) throw new Exception($@"No students found or http error {result.StatusCode}");
        var ret = JsonConvert.DeserializeObject<List<UserCanvasDto>>(result.Result) ?? new List<UserCanvasDto>();
        return ret;
    }
}

//https://fhict.instructure.com:443/api/v1/courses/3166/users?enrollment_role=StudentEnrollment&include[]=test_student&enrollment_state[]=active
// https://fhict.instructure.com/doc/api/live#!/courses.json/list_users_in_course_users_get_6