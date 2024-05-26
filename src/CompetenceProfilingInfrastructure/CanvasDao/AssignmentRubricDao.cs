
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace CompetenceProfilingInfrastructure.CanvasDao;

public class AssignmentRubricDao : IAssignmentRubricDao
{
    private readonly HttpClient _client;
    public AssignmentRubricDao(ICanvasGraphQlHttpClient canvasGraphQlHttpClient)
    {
        _client = canvasGraphQlHttpClient.HttpClient;
    }
    
    public int RubricAssociationId(int assignmentId)
    {
        using var graphQlClient = new GraphQLHttpClient(new GraphQLHttpClientOptions(),new NewtonsoftJsonSerializer(), _client);
        if (graphQlClient == null) throw new ArgumentNullException(nameof(graphQlClient));
        var graphQlRequest = new GraphQLRequest
        {
            Query = @"
			    query AssignmentRubricAssociation($id: ID) {
			         assignment(id:$id) {
                             name
                             rubricAssociation {
                              _id
                              hidePoints
                              hideScoreTotal
                              useForGrading
                             }
                        }
			    }",
            OperationName = "AssignmentRubricAssociation",
            Variables = new
            {
                id = assignmentId
            }
        };
        
        var graphQlResponse = graphQlClient.SendQueryAsync<AssignmentRubricCanvasDto>(graphQlRequest).Result;
        if(int.TryParse(graphQlResponse.Data?.Assignment?.RubricAssociationCanvasDto?.Id ?? "0", out int nr))
        {
            return nr;
        }
        return 0;
    }
    public AssignmentRubricCanvasDto RubricAssignment(int assignmentId)
    {
        using var graphQlClient = new GraphQLHttpClient(new GraphQLHttpClientOptions(),new NewtonsoftJsonSerializer(), _client);
        if (graphQlClient == null) throw new ArgumentNullException(nameof(graphQlClient));
        var graphQlRequest = new GraphQLRequest
        {
            Query = @"
			    query AssignmentRubricAssociation($id: ID) {
			         assignment(id:$id) {
                             name
                             rubricAssociation {
                              _id
                              hidePoints
                              hideScoreTotal
                              useForGrading
                             }
                             rubric {
                              criteria {
                                _id
                                description
                                longDescription
                                outcome {
                                    _id
                                }
                               }
                             }
                             course {
                              _id
                             }
                        }
			    }",
            OperationName = "AssignmentRubricAssociation",
            Variables = new
            {
                id = assignmentId
            }
        };
        
        var graphQlResponse = graphQlClient.SendQueryAsync<AssignmentRubricCanvasDto>(graphQlRequest).Result;
        return graphQlResponse.Data;  
    }
}