using Microsoft.AspNetCore.Mvc;

namespace OnlineExamSystem.Controllers
{
   
    public class ErrorController : Controller
    {
        public IActionResult E500(string type)
        {
            ViewBag.Type = type;
            return View();
        }
        public IActionResult E404(string type)
        {
            ViewBag.Type = type;
            return View();
        }
        public IActionResult E403(string type)
        {
            ViewBag.Type = type;
            return View();
        }
    }
}
