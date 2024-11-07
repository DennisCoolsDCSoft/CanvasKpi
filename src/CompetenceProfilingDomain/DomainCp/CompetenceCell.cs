using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class CompetenceCell
{
    public CompetencesHboEnum CompetenceHbo { get; }
    
    //public LevelsEnum LevelLevel { get;} // calc level
    
    private List<OutcomeGroup> _outcomeGroups= [];
    public IReadOnlyList<OutcomeGroup> OutcomeGroups => _outcomeGroups.AsReadOnly();
    
    
    public CompetenceCell(CompetencesHboEnum competenceHbo, IReadOnlyList<OutcomeResult> outcomeResults)
    {
        CompetenceHbo = competenceHbo;

        foreach (LevelsEnum l in Enum.GetValues(typeof(LevelsEnum)))
        {
            var rest = outcomeResults.Where(w => w.Level == l).ToList();
            if(rest.Count>0)_outcomeGroups.Add(new OutcomeGroup(l, rest));
        }
    }
}