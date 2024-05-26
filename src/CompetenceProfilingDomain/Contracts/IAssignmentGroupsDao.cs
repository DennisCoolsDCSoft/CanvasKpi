using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts;

public interface IAssignmentGroupsDao
{
    AssignmentGroupsConnectionCanvasDto GetAssignmentGroupsByCourseId(int courseId);
}