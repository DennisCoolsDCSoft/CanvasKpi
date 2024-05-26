using CompetenceProfilingDomain.Contracts.ModelsCanvas;

namespace CompetenceProfilingDomain.Contracts;

public interface IAssignmentRubricDao
{
    int RubricAssociationId(int assignmentId);
    AssignmentRubricCanvasDto RubricAssignment(int assignmentId);
}