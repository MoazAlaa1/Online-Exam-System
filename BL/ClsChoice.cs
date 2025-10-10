using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.BL
{
    public interface IChoice
    {
        public List<VwQuestionWithChoices> GetChoicesByExamId(int ExamId);
        public List<VwQuestionWithChoices> GetChoicesByQuestionId(int questionId);
        public List<Choice> GetByQuestionId(int id);
        public bool Save(List<Choice> choices, int questionId);
    }
    public class ClsChoice : IChoice
    {
        ExamSystemContext context;
        public ClsChoice(ExamSystemContext ctx)
        {
            context = ctx;
        }
        public List<VwQuestionWithChoices> GetChoicesByExamId(int ExamId)
        {
            try
            {
                return context.Vw_Question_Choices.Where(a => a.CurrentState == 1 && a.ExamId==ExamId).ToList();
            }
            catch (Exception)
            {
                return new List<VwQuestionWithChoices>();
            }
        }
        public List<VwQuestionWithChoices> GetChoicesByQuestionId(int questionId)
        {
            try
            {
                return context.Vw_Question_Choices.Where(a => a.CurrentState == 1 && a.QuestionId==questionId).ToList();
            }
            catch (Exception)
            {
                return new List<VwQuestionWithChoices>();
            }
        }
        public List<Choice> GetByQuestionId(int id)
        {
            try
            {
                return context.TbChoices.Where(a => a.QuestionId == id).ToList();
            }
            catch
            {
                return new List<Choice>();
            }

        }
       
        public bool Save(List<Choice> choices, int questionId)
        {
            try
            {
                for(int i = 0; i < choices.Count;i++)
                {
                    if (choices[i].ChoiceId != 0)
                    {
                        choices[i].QuestionId = questionId;
                        context.Entry(choices[i]).State = EntityState.Modified;
                    }
                    else
                    {
                        choices[i].QuestionId = questionId;
                        context.TbChoices.Add(choices[i]);
                    }

                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
    }
}
