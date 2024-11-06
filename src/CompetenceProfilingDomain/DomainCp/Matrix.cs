using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class Matrix
{
    private readonly IOutcomeResultCollection _outcomesCollection;
    
    
    private List<Architecture> _architectures = [];
    public IReadOnlyList<Architecture> Architectures => _architectures.AsReadOnly();

    
    public Matrix( IOutcomeResultCollection outcomesCollection)
    {
        _outcomesCollection = outcomesCollection;
    }
    
    
    
    //fill _architectures
    public List<OutcomeResult> FillMatrix(int courseId, int assignmentId, int userId)
    {
        var cards = _outcomesCollection.GetAllCards(courseId, assignmentId, userId);


        foreach (ArchitectureHboEnum a in Enum.GetValues(typeof(ArchitectureHboEnum)))
        {
            var rest = cards.Where(w => w.ArchitectureHbo == a).ToList();
            if(rest.Count >0)_architectures.Add(new Architecture(a,rest)
            );
        }

        return cards;
    }


    // get result by student => OutcomeResultCollection
}