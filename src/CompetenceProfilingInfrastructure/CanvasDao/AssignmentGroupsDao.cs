
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.Infrastructure;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace CompetenceProfilingInfrastructure.CanvasDao;

public class AssignmentGroupsDao : IAssignmentGroupsDao
{
    private HttpClient _client;

    public AssignmentGroupsDao(ICanvasGraphQlHttpClient canvasGraphQlHttpClient)
    {
        _client = canvasGraphQlHttpClient.HttpClient;
    }

    public AssignmentGroupsConnectionCanvasDto GetAssignmentGroupsByCourseId(int courseId)
    {
        using var graphQlClient =
            new GraphQLHttpClient(new GraphQLHttpClientOptions(), new NewtonsoftJsonSerializer(), _client);
        if (graphQlClient == null) throw new ArgumentNullException(nameof(graphQlClient));
        var graphQlRequest = new GraphQLRequest
        {
            Query = @"
            query GetAssignmentGroups($id: ID) {
                course(id: $id ) {
                    assignmentGroupsConnection(first: 100, last: 100) {
                        edges {
                            cursor
                            node {
                                name
                                groupWeight
                                state
                                assignmentsConnection {
                                nodes {
                                    name
                                    _id
                                    }
                                }
                            }
                        }
                    }
                }
            }",
            OperationName = "GetAssignmentGroups",
            Variables = new
            {
                id = courseId
            }
        };

        var graphQlResponse = graphQlClient.SendQueryAsync<AssignmentGroupsCanvasDto>(graphQlRequest).Result;
        return graphQlResponse.Data.CourseCanvasDto.AssignmentGroupsConnectionCanvasDto;
    }
}