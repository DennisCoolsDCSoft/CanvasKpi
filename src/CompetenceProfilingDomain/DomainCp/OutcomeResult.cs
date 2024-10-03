using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeResult
{
    public OutcomeResult(string criteriaId,string outcomeId,string description, string longDescription,ArchitectureHboEnum architectureHbo,CompetencesHboEnum competenceHbo,LevelsEnum level, int levelDivisorNumber)
    {
        CriteriaId = criteriaId;
        OutcomeId = outcomeId;
        Description = description;
        LongDescription = longDescription;

        ArchitectureHbo = architectureHbo;
        CompetenceHbo = competenceHbo;
        Level = level;
        LevelDivisorNumber = levelDivisorNumber;

    }

    public ArchitectureHboEnum ArchitectureHbo { get; }
    public CompetencesHboEnum CompetenceHbo { get; private set; }
    public LevelsEnum Level { get; private set; }
    public int LevelDivisorNumber { get; private set; }
    //public bool Mastered { get; private set; }
    public string Description { get; private set; } = "";
    public string LongDescription { get; private set; } = "";
    
    public string CriteriaId { get; } // criteriaId
    public string OutcomeId { get; } // general id over al courses
    public int? Points { get; set; }  // per student
    
    
    //ToDo ?CourseHistory?
    public List<int> CourseHistory { get; set; } = new();

    // is edit?
}