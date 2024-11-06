using System.Collections.Generic;
using CompetenceProfilingDomain.Definitions;

namespace CanvasKpiLti.Models;

public class OutcomeResultViewModel
{
    public ArchitectureHboEnum ArchitectureHbo { get; set; }
    public CompetencesHboEnum CompetenceHbo { get; set; }
    public LevelsEnum Level { get;  set; }
    public int LevelDivisorNumber { get;  set; }
    public PointScale? Points { get; set; } 
    public string Description { get;  set; } 
    public string LongDescription { get;  set; } 
    public string CriteriaId { get; set; } 
    public string OutcomeId { get; set; }
    public List<int> CourseHistory { get; set; } = new();
    public bool IsEditable { get; set; }
}