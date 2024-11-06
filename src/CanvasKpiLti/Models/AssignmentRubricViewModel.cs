using System.Collections.Generic;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Domain;
using CompetenceProfilingDomain.DomainCp;

namespace CanvasKpiLti.Models;

public class AssignmentRubricViewModel
{
    public AssignmentRubricViewModel(IReadOnlyList<OutcomeResultViewModel> cards, IReadOnlyList<UserCanvasDto> users, int userId, string studentName,
        int assignmentId)
    {
        StudentName = studentName;
        AssignmentId = assignmentId;
        Cards = cards;
        Students = users;
        UserId = userId;
    }
    public IReadOnlyList<OutcomeResultViewModel> Cards { get;}
    public IReadOnlyList<UserCanvasDto> Students { get; }
    public int UserId { get; }

    public string StudentName { get; }
    public int AssignmentId { get; }
}