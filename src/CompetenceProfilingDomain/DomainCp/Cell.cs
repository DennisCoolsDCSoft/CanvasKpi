using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class Cell
{
    public LevelsEnum LevelLevel { get;}
    public IReadOnlyList<OutcomeGroup> OutcomeGroups { get; }

    public CompetencesHboEnum CompetenceHbo { get; }
    public Cell(IReadOnlyList<OutcomeGroup> outcomeGroups)
    {
        OutcomeGroups = outcomeGroups;
    }
    
    
}