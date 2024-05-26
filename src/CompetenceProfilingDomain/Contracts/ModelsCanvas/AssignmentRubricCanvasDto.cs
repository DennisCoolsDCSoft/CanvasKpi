using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class AssignmentRubricCanvasDto
{
    [JsonProperty("assignment")]
    public AssignmentCanvasDto? Assignment { get; set; }
}