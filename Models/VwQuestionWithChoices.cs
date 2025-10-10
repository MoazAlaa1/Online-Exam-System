using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class VwQuestionWithChoices
    {
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; } 
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public int CurrentState { get; set; }
        public int ExamId { get; set; }

    }
}
