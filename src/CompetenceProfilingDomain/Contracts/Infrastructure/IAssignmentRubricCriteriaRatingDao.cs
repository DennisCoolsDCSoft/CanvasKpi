using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts.Infrastructure;

public interface IAssignmentRubricCriteriaRatingDao
{
    void CreateAndUpdateRubricSubmissions(int courseId,int studentId,int rubricAssociationId, Dictionary<string,int?> criterionIdAndPoint);
    SubmissionCanvasDto? SubmissionsByCourseAndAssignmentAndUser(int courseId, int assignmentId,int studentId);
}