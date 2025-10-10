namespace OnlineExamSystem.Classes
{
    public class ExamData
    {
        public ExamData()
        {
            QuestionsAnswers = new List<QuestionAnswer>();
        }
        public List<QuestionAnswer> QuestionsAnswers { get; set; }
    }

    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            Options = new List<Option>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Option> Options { get; set; }
        public string CorrectAnswerId { get; set; }
    }

    public class Option
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class AnswerRequest
    {
        public int QuestionId { get; set; }
        public string ChoiceId { get; set; }
    }

}
