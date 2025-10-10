using OnlineExamSystem.Models;
namespace OnlineExamSystem.Classes
{
    public class MCQ
    {
        public MCQ()
        {
            choices = new List<Choice> { new Choice(),new Choice(), new Choice(),new Choice()};
            question = new Question();
        }
        public Question question { get; set; }
        public List<Choice> choices { get; set; }
    }
}
