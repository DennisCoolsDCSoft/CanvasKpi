using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class RubricCanvasDto
{
    [JsonProperty("criteria")] public List<CriterionCanvasDto> Criteria { get; set; } = new List<CriterionCanvasDto>();
}