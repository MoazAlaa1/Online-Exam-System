using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.BL
{
    public interface ISubmission
    {
        public List<VwSubmission> GetByUserID(string userId);
        public bool Save(submission model);
    }
    public class ClsSubmission : ISubmission
    {
        ExamSystemContext context;
        public ClsSubmission(ExamSystemContext ctx)
        {
            context = ctx;
        }
        
        public List<VwSubmission> GetByUserID(string userId)
        {
            try
            {
                return context.VwSubmissions.Where(a => a.CurrentState == 1 && a.UserId==userId).ToList();
            }
            catch (Exception ex)
            {
                return new List<VwSubmission>();
            }
        }

        public bool Save(submission model)
        {
            try
            {

                if (model.SubmissionId != 0)
                {                 
                    context.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.CurrentState = 1;
                    model.SubmissionDate = DateTime.Now;
                    context.TbSubmissions.Add(model);
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
