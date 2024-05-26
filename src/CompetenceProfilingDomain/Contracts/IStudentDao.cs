using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts;

public interface IStudentDao
{
    IEnumerable<UserCanvasDto> GetStudentsInCourse(int courseId);
}