using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class AssignmentGroupsConnectionCanvasDto
{
    [JsonProperty("edges")] public List<EdgeCanvasDto> Edges { get; set; } = new();
}