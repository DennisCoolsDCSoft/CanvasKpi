using CompetenceProfilingDomain.Definitions;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeGroup
{
    public LevelsEnum Level { get; }
    //public bool MasteredOutcomeGroup { get; }
    public IReadOnlyList<OutcomeResult> OutcomeResults { get;}

    public OutcomeGroup(LevelsEnum level,IReadOnlyList<OutcomeResult> outcomeResultsForThisGroup)
    {
        Level = level;
        OutcomeResults = outcomeResultsForThisGroup;
      //  MasteredOutcomeGroup = false;
        HistoryOnOutcomeIdOrCriteriaId();
    }
    
    private void HistoryOnOutcomeIdOrCriteriaId()
    {
        foreach (var outcomeResult in OutcomeResults.Where(w=>w.IsEditable))
        {
            var outcomeResultCourseHistory = OutcomeResults.Where(w=>w.OutcomeId == outcomeResult.OutcomeId && w.Points == PointScale.Mastered 
                                                                     || w.CriteriaId == outcomeResult.CriteriaId && w.Points == PointScale.Mastered)
                    .Select(s=>s.CourseId).ToList();
            
            if (outcomeResultCourseHistory.Count != 0)
                outcomeResult.CourseHistory = outcomeResultCourseHistory.Where(d=>d != outcomeResult.CourseId).ToList();
            else
                if(CalcHistory(outcomeResult))
                    outcomeResult.CourseHistory.Add(1);
        }
    }

    private bool CalcHistory(OutcomeResult outcomeResult)
    {
        if(outcomeResult.LevelDivisorNumber != 0) return false;
        
        var distinctByDescription = OutcomeResults.Where(w=>w.OutcomeId != outcomeResult.OutcomeId).DistinctBy(d => d.Description);
        var checkList = new List<Tuple<string, bool>>();
    
        foreach (var outcomeResultdistinct in distinctByDescription)
        {
            var mastered = OutcomeResults.FirstOrDefault(f => f.Mastered && f.Description == outcomeResultdistinct.Description)?.Mastered ?? false; 
            checkList.Add(new Tuple<string, bool>(outcomeResultdistinct.Description,mastered));
        }
        if (checkList.Count is 0) 
            return false;
        
        
        var nr = checkList.Count / 2.00;
        var countTrue = checkList.Count(w => w.Item2 == true);
        var ret = nr <= countTrue;
        return ret;
    }
    
    
    // private bool CalcHalve()
    // {
    //     //todo calc
    //     //allUnique
    //     //if (OutcomeResults.Count != OutcomeResults.DistinctBy(d=>d.LevelDivisorNumber).Count()) 
    //     //    if(throw new Exception("Same LevelDivisorNumber in level group found");
    //     
    //     var distDes = OutcomeResults.DistinctBy(d => d.Description);
    //     var checkList = new List<Tuple<string, bool>>();
    //
    //     foreach (var d in distDes.Where(w=>w.LevelDivisorNumber >0))
    //     {
    //         var check = OutcomeResults.FirstOrDefault(f => f.Mastered && f.Description == d.Description)?.Mastered ?? false; 
    //         checkList.Add(new Tuple<string, bool>(d.Description,check));
    //     }
    //
    //     var nr = checkList.Count / 2.00;
    //     var countTrue = checkList.Count(w => w.Item2 == true);
    //     var ret = nr <= countTrue;
    //     return ret;
    // }
    
}