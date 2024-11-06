using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts.Infrastructure;

public interface IAssignmentGroupsDao
{
    AssignmentGroupsConnectionCanvasDto GetAssignmentGroupsByCourseId(int courseId);
}