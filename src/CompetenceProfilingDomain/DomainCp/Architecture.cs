using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class Architecture
{
    public ArchitectureHboEnum ArchitectureHbo { get; }


    private readonly List<CompetenceCell> _competenceCells = [];
    public IReadOnlyList<CompetenceCell> CompetenceCells => _competenceCells.AsReadOnly();
    
    public Architecture (ArchitectureHboEnum architectureHboEnum, IReadOnlyList<OutcomeResult> outcomeResults)
    {
        ArchitectureHbo = architectureHboEnum;
        
        foreach (CompetencesHboEnum c in Enum.GetValues(typeof(CompetencesHboEnum)))
        {
            var rest = outcomeResults.Where(w => w.CompetenceHbo == c).ToList();
            if(rest.Count > 0) _competenceCells.Add(new CompetenceCell(c,rest));
        }
    }
}