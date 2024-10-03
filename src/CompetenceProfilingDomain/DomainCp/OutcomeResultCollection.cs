using System.Diagnostics;
using System.Globalization;
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Contracts.ModelsDatabase;
using CompetenceProfilingDomain.Definitions;
using CompetenceProfilingDomain.Domain;
using CompetenceProfilingDomain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CompetenceProfilingDomain.DomainCp;

public class OutcomeResultCollection
{
    private readonly IAssignmentRubricCriteriaRatingDao _assignmentRubricCriteriaRatingDao;
    private readonly IRepository _repository;
    private readonly IDistributedCache _distributedCache;
    private readonly IAssignmentRubricDao _assignmentRubricDao;

    // add outcomes from assessment rubric + results in course   (edit = true)
    // add outcomes from history database (readonly edit = false)

    public OutcomeResultCollection(IAssignmentRubricCriteriaRatingDao assignmentRubricCriteriaRatingDao, IRepository repository,IDistributedCache distributedCache,IAssignmentRubricDao assignmentRubricDao)
    {
        _assignmentRubricCriteriaRatingDao = assignmentRubricCriteriaRatingDao;
        _repository = repository;
        _distributedCache = distributedCache;
        _assignmentRubricDao = assignmentRubricDao;
    }
    

    public List<OutcomeResult> GetAllCards(int courseId, int assignmentId, int userId)
    {
        Debug.WriteLine($"GetAllCardsStart {DateTime.Now.ToString("h:mm:ss.fff")}");
        RubricAssociationId(assignmentId); // check rubric number
        var rubricCriteria = RubricCriteria(assignmentId);
        
        //todo sql slow
        var submissionsCanvas = _assignmentRubricCriteriaRatingDao.SubmissionsByCourseAndAssignmentAndUser(courseId, assignmentId, userId);
        var submissionsStudent = _repository.Query<StudentAdviceDto>()
            .Where(w => w.CourseId == courseId & w.UserId == userId).ToList();
        var studKpi = _repository.Query<StudentKpiDto>().Where(w => w.UserId == userId && w.CourseId != courseId && w.Point != null).ToList();
        var outcomes = _repository.Query<OutcomesCanvasDto>().ToList();
        Debug.WriteLine($"GetAllCardsEnd sql {DateTime.Now.ToString("h:mm:ss.fff")}");
        
        var ret = new List<OutcomeResult>();
        foreach (var criteria in rubricCriteria)
        {
            var subCan = submissionsCanvas?.RubricAssessment.FirstOrDefault(f => f._id == criteria.Id);
            var subStud = submissionsStudent.FirstOrDefault(f=>f.CriteriaId== criteria.Id);
            var outcome = outcomes.FirstOrDefault(f => f.LmsId == criteria.OutcomeCanvasDto.Id || f.CriteriaId == criteria.Id);
            
            OutcomeResult card;
            if (outcome == null)
            {
                card = new OutcomeResult(criteria.Id, criteria.OutcomeCanvasDto.Id, criteria.Description, criteria.LongDescription,ArchitectureHboEnum.NotFound,CompetencesHboEnum.NotFound,LevelsEnum.NotFound,0);
            }
            else
            {
                card = new OutcomeResult(criteria.Id, criteria.OutcomeCanvasDto.Id, criteria.Description, criteria.LongDescription,outcome.Architecture,outcome.Competence,outcome.Level,outcome.LevelDivisorNumber);
            }
            
            
            if(subStud!= null) // stud advises geel
            {
                if(subStud.Point== (int)PointScale.StudentAdvice)
                    card.Points = subStud.Point;
            }

            if (subCan?.Points is "3" or "4" or "5")  // canvas scale
            {
                card.Points = (int)PointScale.Mastered;
            }

            card.CourseHistory = studKpi.Where(w 
                => w.OutcomeId == criteria.OutcomeCanvasDto.Id && w.Point == (int)PointScale.Mastered
                   || w.CriteriaId == criteria.Id && w.Point==(int)PointScale.Mastered
                
            ).Select(s=>s.CourseId).ToList();
            
            ret.Add(card);
        }
        Debug.WriteLine($"GetAllCardsEnd {DateTime.Now.ToString("h:mm:ss.fff")}");
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