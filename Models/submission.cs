using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineExamSystem.BL;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class submission
    {
        public submission()
        {
            
        }
        [Key]
        [ValidateNever]
        public int SubmissionId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public int ExamId { get; set; }
        [Range(0,100)]
        public float Score { get; set; }
        [ValidateNever]
        public string Status { get; set; }
        [ValidateNever]
        public DateTime SubmissionDate { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; } = 1;
        [ValidateNever]
        public ApplicationUser TbUser { get; set; }
        [ValidateNever]
        public Exam TbExam { get; set; }

    }
}
