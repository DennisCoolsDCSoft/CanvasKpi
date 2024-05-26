using System.Collections.Generic;
using CompetenceProfilingDomain.Domain;

namespace CanvasKpiLti.Models;

public class AssignmentRubricCriteriaRatingViewModel
{
    public int UserId { get; set; }
    public int AssignmentId { get; set; }
    public List<PointViewModel> Points { get; set; } = new ();
}