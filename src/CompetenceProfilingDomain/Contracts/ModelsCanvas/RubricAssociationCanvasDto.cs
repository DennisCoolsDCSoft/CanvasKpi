using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class RubricAssociationCanvasDto
{
    [JsonProperty("_id")] public string Id { get; set; } = "";

    [JsonProperty("hidePoints")]
    public bool HidePoints { get; set; }

    [JsonProperty("hideScoreTotal")]
    public bool HideScoreTotal { get; set; }

    [JsonProperty("useForGrading")]
    public bool UseForGrading { get; set; }
}