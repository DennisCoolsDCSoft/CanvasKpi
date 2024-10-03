using System.ComponentModel.DataAnnotations;
using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.Contracts.ModelsDatabase;

public class OutcomesCanvasDto
{
    [Key]
    public string LmsId { get; set; }
    public string CriteriaId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ArchitectureHboEnum Architecture { get; set; }
    public CompetencesHboEnum Competence { get; set; }
    public LevelsEnum Level { get; set; }
    public int LevelDivisorNumber { get; set; }
    
    public virtual ICollection<TreeRootCanvasDto> TreeRootsCanvas { get; set; }
}