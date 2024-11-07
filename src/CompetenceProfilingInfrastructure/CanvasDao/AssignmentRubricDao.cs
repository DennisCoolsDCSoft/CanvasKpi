using CompetenceProfilingDomain.Contracts.Infrastructure;
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
        using var graphQlClient =
            new GraphQLHttpClient(new GraphQLHttpClientOptions(), new NewtonsoftJsonSerializer(), _client);
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
        if (int.TryParse(graphQlResponse.Data?.Assignment?.RubricAssociationCanvasDto?.Id ?? "0", out int nr))
        {
            return nr;
        }

        return 0;
    }

    /// <summary>
    /// https://xxx.instructure.com/graphiql
    /// query AssignmentRubricAssociation() {
    ///     assignment(id: 214628) {
    ///         name
    ///         rubricAssociation {
    ///             _id
    ///                 hidePoints
    ///             hideScoreTotal
    ///                 useForGrading
    ///         }
    ///         rubric {
    ///             criteria {
    ///                 _id
    ///                     description
    ///                 longDescription
    ///                 outcome {
    ///                     _id
    ///                 }
    ///             }
    ///         }
    ///         course {
    ///             _id
    ///         }
    ///     }
    /// }
    ///
    ///
    /// 
    ///     "data": {
    ///         "assignment": {
    ///             "name": "Jouw beoordeling",
    ///             "rubricAssociation": {
    ///                 "_id": "64615",
    ///                 "hidePoints": true,
    ///                 "hideScoreTotal": false,
    ///                 "useForGrading": false
    ///             },
    ///             "rubric": {
    ///                 "criteria": [
    ///                 {
    ///                     "_id": "_5759",
    ///                     "description": "Analysis-H1.1",
    ///                     "longDescription": "<p><strong>EN</strong><br>Describe the architecture of a computer system.</p>\n<p><strong>NL</strong><br>Beschrijven van de architectuur van een computersysteem.</p>",
    ///                     "outcome": {
    ///                         "_id": "6773"
    ///                     }
    ///                 },
    ///                 {
    ///                     "_id": "35984_6579",
    ///                     "description": "IPS 2.3",
    ///                     "longDescription": "<p><span>• You actively look for alternatives.</span></p>\n<p><span>• Je zoekt actief naar alternatieven.</span></p>\n<p>&nbsp;</p>",
    ///                     "outcome": {
    ///                         "_id": "20363"
    ///                     }
    ///                 }
    ///                 ]
    ///             },
    ///             "course": {
    ///                 "_id": "12842"
    ///             }
    ///         }
    ///     }
    /// }
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public AssignmentRubricCanvasDto RubricAssignment(int assignmentId)
    {
        using var graphQlClient =
            new GraphQLHttpClient(new GraphQLHttpClientOptions(), new NewtonsoftJsonSerializer(), _client);
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