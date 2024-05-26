using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class CourseCanvasDto
{
    [JsonProperty("_id")] public string? Id { get; set; }

    [JsonProperty("assignmentGroupsConnection")]
    public AssignmentGroupsConnectionCanvasDto AssignmentGroupsConnectionCanvasDto { get; set; } = new();
}