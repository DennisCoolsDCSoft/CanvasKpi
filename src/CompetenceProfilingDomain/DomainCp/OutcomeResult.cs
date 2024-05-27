using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeResult
{
    public ArchitectureHboEnum ArchitectureHbo { get; private set; }
    public CompetencesHboEnum CompetenceHbo { get; private set; }
    public LevelsEnum Level { get; private set; }
    public int LevelDivisorNumber { get; private set; }
    public bool Mastered { get; private set; }
    public string Description { get; private set; } = "";
    public string LongDescription { get; private set; } = "";
    
    public int? LmsId { get; set; } = null;
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public string TermName { get; set; }
    
    
    // is edit?
}