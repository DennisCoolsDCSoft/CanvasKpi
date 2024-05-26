using System.ComponentModel.DataAnnotations;

namespace CompetenceProfilingDomain.Contracts.models
{
    public class StudentAdvice
    {
        [Key]
        public int Id { get; set; }

        public string CriteriaId { get; set; }

        public int UserId { get; set; }

        public int Point { get; set; }

        public int CourseId { get; set; }

        public string OutcomeId { get; set; }
    }
}
