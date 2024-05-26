using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class EdgeCanvasDto
{
    [JsonProperty("cursor")] public string Cursor { get; set; } = "";
    [JsonProperty("node")] public NodeCanvasDto NodeCanvasDto { get; set; } = new();
}