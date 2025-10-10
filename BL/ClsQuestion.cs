using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.BL
{
    public interface IQuestion
    {
        public List<Question> GetAll();
        public Question GetById(int id);
        public bool Save(Question question,List<Choice>choices);
        public bool Delete(int id);
    }
    public class ClsQuestion : IQuestion
    {
        ExamSystemContext context;
        IChoice _ClsChoice;
        public ClsQuestion(ExamSystemContext ctx, IChoice clsChoice)
        {
            context = ctx;
            _ClsChoice = clsChoice;
        }
        public List<Question> GetAll()
        {
            try
            {
                return context.TbQuestions.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<Question>();
            }

        }
        public Question GetById(int id)
        {
            try
            {
                return context.TbQuestions.Where(a => a.QuestionId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new Question();
            }
        }
        public bool Save(Question question, List<Choice> choices)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {

                if (question.QuestionId != 0)
                {
                    question.UpdateDate = DateTime.Now;
                    question.UpdatedBy = "Admin";
                    context.Entry(question).State = EntityState.Modified;
                }
                else
                {
                    question.CurrentState = 1;
                    question.CreateDate = DateTime.Now;
                    question.CreatedBy = "Admni";
                    context.TbQuestions.Add(question);
                }
                context.SaveChanges();
                _ClsChoice.Save(choices , question.QuestionId);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var Question = GetById(id);
                Question.CurrentState = 0;
                context.Entry(Question).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
