using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeGroup
{
    public LevelsEnum Level { get; }
    public bool MasteredOutcomeGroup { get; }
    public IReadOnlyList<OutcomeResult> OutcomeResults { get;}

    public OutcomeGroup(IReadOnlyList<OutcomeResult> outcomeResultsForThisGroup)
    {
        OutcomeResults = outcomeResultsForThisGroup;
    }
    
    // private bool CalcHalve()
    // {
    //     var distDes = OutcomeResults.DistinctBy(d => d.Description);
    //     var checkList = new List<Tuple<string, bool>>();
    //
    //     foreach (var d in distDes)
    //     {
    //         var check = OutcomeResults.FirstOrDefault(f => f.Mastered && f.Description == d.Description)?.Mastered ?? false; 
    //         checkList.Add(new Tuple<string, bool>(d.Description,check));
    //     }
    //
    //     var nr = checkList.Count / 2.00;
    //     var countTrue = checkList.Count(w => w.Item2);
    //     var ret = nr <= countTrue;
    //     return ret;
    // }
    
}