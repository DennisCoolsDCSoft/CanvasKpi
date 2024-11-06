using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts.Infrastructure;

public interface IStudentDao
{
    IEnumerable<UserCanvasDto> GetStudentsInCourse(int courseId);
}