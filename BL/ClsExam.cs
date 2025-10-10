using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Models;

namespace OnlineExamystem.BL
{
    public interface IExam
    {
        public List<Exam> GetAll();
        public List<Exam> GetAvailableExams();
        public Exam GetById(int id);
        public bool Save(Exam model);
        public bool Delete(int id);
    }
    public class ClsExam : IExam
    {
        ExamSystemContext context;
        public ClsExam(ExamSystemContext ctx)
        {
            context = ctx;
        }
        public List<Exam> GetAll()
        {
            try
            {
                return context.TbExams.Where(a=>a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<Exam>();
            }

        }
        public List<Exam> GetAvailableExams()
        {
            try
            {
                return context.TbExams.Where(a=>a.CurrentState == 1 && a.isPublish == true).ToList();
            }
            catch
            {
                return new List<Exam>();
            }

        }
        public Exam GetById(int id)
        {
            try
            {
                return context.TbExams.Where(a => a.ExamId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new Exam();
            }
        }
        public bool Save(Exam model)
        {
            try
            {

                if (model.ExamId != 0)
                {
                    model.UpdateDate = DateTime.Now;
                    model.UpdatedBy =  "Admin";
                    context.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.CurrentState = 1;
                    model.CreateDate = DateTime.Now;
                    model.CreatedBy = "Admin";
                    context.TbExams.Add(model);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var Exam = GetById(id);
                Exam.CurrentState = 0;
                context.Entry(Exam).State = EntityState.Modified;
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
