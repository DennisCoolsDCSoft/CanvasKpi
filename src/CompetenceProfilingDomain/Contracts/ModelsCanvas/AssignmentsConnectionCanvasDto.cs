using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class AssignmentsConnectionCanvasDto
{
    [JsonProperty("nodes")] public List<NodeAssignmentsConnectionCanvasDto> Nodes { get; set; } = new();
}