using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class AssignmentGroupsCanvasDto
{
    [JsonProperty("course")] public CourseCanvasDto CourseCanvasDto { get; set; } = new();
}