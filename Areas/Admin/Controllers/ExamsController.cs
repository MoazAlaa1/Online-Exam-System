using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.Models;
using OnlineExamystem.BL;

namespace OnlineExamSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExamsController : Controller
    {
        IExam _ClsExam;
        public ExamsController(IExam ClsExam)
        {
            _ClsExam = ClsExam;
        }
        public IActionResult List()
        {
            return View(_ClsExam.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null || id == 0)
            {
                return View(_ClsExam.GetById(Convert.ToInt32(id)));
            }
            return View(new Exam());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Exam model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = _ClsExam.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = _ClsExam.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }

        
    }
}
