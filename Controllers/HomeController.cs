using Microsoft.AspNetCore.Mvc;
using OnlineExamystem.BL;

namespace OnlineExamSystem.Controllers
{
    public class HomeController : Controller
    {
        IExam _ClsExam;
        public HomeController(IExam ClsExam)
        {
            _ClsExam = ClsExam;
        }
        public IActionResult Index()
        {
            return View(_ClsExam.GetAvailableExams());
        }

        
    }
}
