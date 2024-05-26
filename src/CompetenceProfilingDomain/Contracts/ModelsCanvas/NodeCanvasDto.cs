using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class NodeCanvasDto
{
    [JsonProperty("name")] public string Name { get; set; } = "";
    [JsonProperty("groupWeight")] public string GroupWeight { get; set; } = "";
    [JsonProperty("state")] public string State { get; set; } = "";

    [JsonProperty("assignmentsConnection")]
    public AssignmentsConnectionCanvasDto AssignmentsConnectionCanvasDto { get; set; } = new();
}