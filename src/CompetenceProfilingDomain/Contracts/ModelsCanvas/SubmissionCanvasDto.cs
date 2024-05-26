using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class SubmissionCanvasDto
{

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("body")] public object Body { get; set; }

    [JsonProperty("url")] public object Url { get; set; }

    [JsonProperty("grade")] public object Grade { get; set; }

    [JsonProperty("score")] public object Score { get; set; }

    [JsonProperty("submitted_at")] public object SubmittedAt { get; set; }

    [JsonProperty("assignment_id")] public int AssignmentId { get; set; }

    [JsonProperty("user_id")] public int UserId { get; set; }

    [JsonProperty("submission_type")] public object SubmissionType { get; set; }

    [JsonProperty("workflow_state")] public string WorkflowState { get; set; }

    [JsonProperty("grade_matches_current_submission")]
    public bool GradeMatchesCurrentSubmission { get; set; }

    [JsonProperty("graded_at")] public object GradedAt { get; set; }

    [JsonProperty("grader_id")] public object GraderId { get; set; }

    [JsonProperty("attempt")] public object Attempt { get; set; }

    [JsonProperty("cached_due_date")] public object CachedDueDate { get; set; }

    [JsonProperty("excused")] public object Excused { get; set; }

    [JsonProperty("late_policy_status")] public object LatePolicyStatus { get; set; }

    [JsonProperty("points_deducted")] public object PointsDeducted { get; set; }

    [JsonProperty("grading_period_id")] public object GradingPeriodId { get; set; }

    [JsonProperty("extra_attempts")] public object ExtraAttempts { get; set; }

    [JsonProperty("posted_at")] public object PostedAt { get; set; }

    [JsonProperty("redo_request")] public bool RedoRequest { get; set; }

    [JsonProperty("late")] public bool Late { get; set; }

    [JsonProperty("missing")] public bool Missing { get; set; }

    [JsonProperty("seconds_late")] public int SecondsLate { get; set; }

    [JsonProperty("entered_grade")] public object EnteredGrade { get; set; }

    [JsonProperty("entered_score")] public object EnteredScore { get; set; }

    [JsonProperty("preview_url")] public string PreviewUrl { get; set; }

   // [JsonProperty("rubric_assessment")]
   public List<RubricAssessmentSubmissionCanvasDto> RubricAssessment { get; set; } = new List<RubricAssessmentSubmissionCanvasDto>();

    [JsonProperty("anonymous_id")] public string AnonymousId { get; set; }
}

// "_6505": {
//     "rating_id": "blank",
//     "comments": "",
//     "points": 5.0
// },
