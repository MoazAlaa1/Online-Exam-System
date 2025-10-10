using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExamystem.BL;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Controllers
{
    public class ExamController : Controller
    {
        IExam _ClsExam;
        public ExamController(IExam ClsExam)
        {
            _ClsExam = ClsExam;
        }
        public IActionResult AvailableExams()
        {
            return View(_ClsExam.GetAvailableExams());
        }
        [Authorize]
        public IActionResult StartExam(int ExamId)
        {
            var exam = _ClsExam.GetById(Convert.ToInt32(ExamId));
            if(exam == null)
                return Redirect("/Error/E404?type=Admin");
            return View(exam);
        }
    }
}
