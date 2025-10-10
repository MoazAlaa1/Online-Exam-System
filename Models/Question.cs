using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class Question
    {
        public Question()
        {

            TbChoice = new HashSet<Choice>();
        }
        [Key]
        [ValidateNever]
        public int QuestionId { get; set; }
        [Required(ErrorMessage ="Enter The Question Title")]
        public string QuestionTitle { get; set; }
        [ValidateNever]
        public DateTime? CreateDate { get; set; }
        [ValidateNever]
        public string? CreatedBy { get; set; } = "";
        [ValidateNever]
        public DateTime? UpdateDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; } = "";
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
        [ValidateNever]
        public int ExamId { get; set; } = 0;
        [ValidateNever]
        public Exam TbExam { get; set; }
        [ValidateNever]
        public ICollection<Choice> TbChoice { get; set; }
    }
}
