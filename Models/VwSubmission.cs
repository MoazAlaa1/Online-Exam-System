using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class VwSubmission
    {
        public int SubmissionId { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public float Score { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int CurrentState { get; set; } = 1;
        public string ExamTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
