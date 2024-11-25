using CompetenceProfilingDomain.Contracts.Infrastructure;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Contracts.ModelsDatabase;
using CompetenceProfilingDomain.Definitions;
using CompetenceProfilingDomain.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeResultCollection : IOutcomeResultCollection
{
    private readonly IAssignmentRubricCriteriaRatingDao _assignmentRubricCriteriaRatingDao;
    private readonly IRepository _repository;
    private readonly IDistributedCache _distributedCache;
    private readonly IAssignmentRubricDao _assignmentRubricDao;
    
    public OutcomeResultCollection(IAssignmentRubricCriteriaRatingDao assignmentRubricCriteriaRatingDao, IRepository repository,IDistributedCache distributedCache,IAssignmentRubricDao assignmentRubricDao)
    {
        _assignmentRubricCriteriaRatingDao = assignmentRubricCriteriaRatingDao;
        _repository = repository;
        _distributedCache = distributedCache;
        _assignmentRubricDao = assignmentRubricDao;
    }
    
    /// <summary>
    /// Debugging and exporting data byHand
    /// --20360,"IPS 2.1","<p><span>• You determine the direction ...</span></p>",50,25,2,1,35984_4978
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    public string PrintCriterionForDatabaseTableOutcomeCanvas(int assignmentId)
    {
        var rubricCriteria = RubricCriteria(assignmentId);

        string buff="";
        foreach (var r in rubricCriteria.Where(d=>d.Description.StartsWith("TI")))
        {
            buff = buff +'\n' + $"--{r.OutcomeCanvasDto.Id},\"{r.Description}\",\"{r.LongDescription}\",0,0,0,0,{r.Id}";
        }

        return buff;
    }
    
    public List<OutcomeResult> GetAllCards(int courseId, int assignmentId, int userId)
    {
        //Debug.WriteLine($"GetAllCardsStart {DateTime.Now.ToString("h:mm:ss.fff")}");
        RubricAssociationId(assignmentId); // check rubric number
        
        var rubricCriteria = RubricCriteria(assignmentId);
        
        var submissionsCanvas = _assignmentRubricCriteriaRatingDao.SubmissionsByCourseAndAssignmentAndUser(courseId, assignmentId, userId);
        var submissionsStudent = _repository.Query<StudentAdviceDto>()
            .Where(w => w.CourseId == courseId & w.UserId == userId).ToList();
        var outcomes = _repository.Query<OutcomesCanvasDto>().ToList();
        
        // rubric course 
        var ret = new List<OutcomeResult>();
        foreach (var criteria in rubricCriteria)
        {
            var subCan = submissionsCanvas?.RubricAssessment.FirstOrDefault(f => f._id == criteria.Id);
            var subStud = submissionsStudent.FirstOrDefault(f=>f.CriteriaId== criteria.Id);
            var outcome = outcomes.FirstOrDefault(f => f.LmsId == criteria.OutcomeCanvasDto.Id || f.CriteriaId == criteria.Id);
            
            OutcomeResult card;
            if (outcome == null)
            {
                card = new OutcomeResult(criteria.Id, criteria.OutcomeCanvasDto.Id, criteria.Description, criteria.LongDescription,ArchitectureHboEnum.NotFound,CompetencesHboEnum.NotFound,LevelsEnum.NotFound,0,courseId);
                card.IsEditable = false;
            }
            else
            {
                card = new OutcomeResult(criteria.Id, criteria.OutcomeCanvasDto.Id, criteria.Description, criteria.LongDescription,outcome.Architecture,outcome.Competence,outcome.Level,outcome.LevelDivisorNumber,courseId);
                card.IsEditable = true;
            }
            
            if(subStud!= null) // stud advises geel
            {
                if(subStud.Point== (int)PointScale.StudentAdvice)
                    card.Points = (PointScale?)subStud.Point;
            }

            if (subCan?.Points is "3" or "4" or "5")  // canvas scale
            {
                card.Points = PointScale.Mastered;
            }
            
            ret.Add(card);
        }
        
        
        //add history
        var studKpihis = _repository.Query<StudentKpiDto>().Where(w => w.UserId == userId && w.CourseId != courseId).ToList();

        var disstudhis = new List<StudentKpiDto>();

        foreach (var stud in studKpihis.Where(w => w.Point != null))
        {
            disstudhis.Add(stud);
        }

        foreach (var studk in studKpihis)
        {
            if (disstudhis.All(a => a.OutcomeId != studk.OutcomeId))
            {
                if(ret.All(a => a.OutcomeId != studk.OutcomeId))
                    disstudhis.Add(studk);
            }
        }
        
        foreach (var studhis in disstudhis)
        {
            var outcome =
                outcomes.FirstOrDefault(f => f.LmsId == studhis.OutcomeId || f.CriteriaId == studhis.CriteriaId);
            if (outcome!= null)
            {
                var cardhist = new OutcomeResult(studhis.CriteriaId, studhis.OutcomeId, outcome.Title, $"_{studhis.CourseId}_{studhis.Point}", outcome.Architecture, outcome.Competence,outcome.Level,outcome.LevelDivisorNumber,studhis.CourseId);
                cardhist.IsEditable = false;
                cardhist.Points = (PointScale?)studhis.Point;
                ret.Add(cardhist);
            }
        }
        
        //Debug.WriteLine($"GetAllCardsEnd {DateTime.Now.ToString("h:mm:ss.fff")}");
        return ret;
    }

    
   

    private int RubricAssociationId(int assignmentId)
    {
        var buff = _distributedCache.GetDistributedCache<string>("RubricAssociationId" + assignmentId);
        if (buff != null)
        {
            return int.Parse(buff);
        }
        
        var nr = _assignmentRubricDao.RubricAssociationId(assignmentId);
        if (nr == 0) throw new Exception($"No Rubric found for the assignment-> is er een rubric gekoppeld aan de assignment met nummer: {assignmentId}  ");
        
        _distributedCache.SetDistributedCache("RubricAssociationId" + assignmentId,nr.ToString(),15);
        return nr;
    }
    private List<CriterionCanvasDto> RubricCriteria(int assignmentId)
    {
        var buff = _distributedCache.GetDistributedCache<List<CriterionCanvasDto>>("ListCriterion" + assignmentId);
        if (buff != null)
        {
            return buff;
        }
        
        var data = _assignmentRubricDao.RubricAssignment(assignmentId);
        var rubricCriteria = data.Assignment?.RubricCanvasDto.Criteria;
        if (rubricCriteria == null) throw new Exception("Rubric Criteria not found");
        
        _distributedCache.SetDistributedCache("ListCriterion" + assignmentId,rubricCriteria,15);
        return rubricCriteria;
    }
}