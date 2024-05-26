using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class CriterionCanvasDto
{
    [JsonProperty("_id")] public string Id { get; set; } = "";

    [JsonProperty("description")] public string Description { get; set; } = "";

    [JsonProperty("longDescription")] public string LongDescription { get; set; } = "";

    [JsonProperty("outcome")] public OutcomeCanvasDto OutcomeCanvasDto { get; set; } = new OutcomeCanvasDto();
}