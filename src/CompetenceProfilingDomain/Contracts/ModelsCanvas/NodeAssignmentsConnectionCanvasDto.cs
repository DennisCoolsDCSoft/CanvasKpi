using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class NodeAssignmentsConnectionCanvasDto
{
    [JsonProperty("name")] public string Name { get; set; } = "";
    [JsonProperty("_id")] public string Id { get; set; } = "";
}