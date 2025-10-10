using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class Choice
    {
        public Choice()
        {
              
        }
        [Key]
        [ValidateNever]
        public int ChoiceId { get; set; } = 0;
        [Required(ErrorMessage = "Enter The Choice Text")]
        public string ChoiceText { get; set; } = "";
        [Required]
        public bool IsCorrect { get; set; } = false;
        [ValidateNever]
        public int QuestionId { get; set; }
        [ValidateNever]
        public Question TbQuestion { get; set; }

    }
}
