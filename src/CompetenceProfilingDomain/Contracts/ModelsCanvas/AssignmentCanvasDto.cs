using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class AssignmentCanvasDto
{
    [JsonProperty("name")] public string Name { get; set; } = "";

    [JsonProperty("rubricAssociation")]
    public RubricAssociationCanvasDto RubricAssociationCanvasDto { get; set; } = new RubricAssociationCanvasDto();

    [JsonProperty("rubric")] public RubricCanvasDto RubricCanvasDto { get; set; } = new RubricCanvasDto();

    [JsonProperty("course")] public CourseCanvasDto CourseCanvasDto { get; set; } = new CourseCanvasDto();
}