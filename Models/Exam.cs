using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class Exam
    {
        public Exam()
        {
            TbQuestion = new HashSet<Question>();
            TbSubmissin = new HashSet<submission>();
        }
        [Key]
        [ValidateNever]
        public int ExamId { get; set; }
        [Required(ErrorMessage ="Enter The Exam Title")]
        [MaxLength(200)]
        public string ExamTitle { get; set; }
        public string? ExamDescription { get; set; } = "";
        [MaxLength(200)]
        public string? Notes { get; set; } = "";
        public bool isPublish {  get; set; } = false;
        [ValidateNever]
        public DateTime CreateDate { get; set; }
        [ValidateNever]
        public string CreatedBy { get; set; } = "";
        [ValidateNever]
        public DateTime? UpdateDate { get; set; } 
        [ValidateNever]
        public string? UpdatedBy { get; set; } = "";
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
        public ICollection<Question> TbQuestion { get; set; }
        public ICollection<submission> TbSubmissin { get; set; }

    }
}
