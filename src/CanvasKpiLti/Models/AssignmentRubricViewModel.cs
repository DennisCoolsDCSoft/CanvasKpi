using System.Collections.Generic;
using CompetenceProfilingDomain.Contracts.ModelsCanvas;
using CompetenceProfilingDomain.Domain;

namespace CanvasKpiLti.Models;

public class AssignmentRubricViewModel
{
    public AssignmentRubricViewModel(IReadOnlyList<Card> cards, IReadOnlyList<UserCanvasDto> users, int userId, string studentName,
        int assignmentId)
    {
        StudentName = studentName;
        AssignmentId = assignmentId;
        Cards = cards;
        Students = users;
        UserId = userId;
    }
    public IReadOnlyList<Card> Cards { get;}
    public IReadOnlyList<UserCanvasDto> Students { get; }
    public int UserId { get; }

    public string StudentName { get; }
    public int AssignmentId { get; }
}