namespace CompetenceProfilingDomain.DomainCp;

public interface IOutcomeResultCollection
{
    List<OutcomeResult> GetAllCards(int courseId, int assignmentId, int userId);
}