using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.BL;
using OnlineExamSystem.Classes;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        IQuestion _ClsQuestion;
        IChoice _ClsChoice;
        public QuestionController(IQuestion ClsQuestion, IChoice clsChoice)
        {
            _ClsQuestion = ClsQuestion;
            _ClsChoice = clsChoice;
        }
        public IActionResult Edit(int? id,int examId)
        {
            MCQ mcq = new MCQ();
            if (id != null || id==0)
            {
                var result  = _ClsChoice.GetChoicesByQuestionId(Convert.ToInt32(id));
                mcq.question = new Question
                {
                    QuestionId = result[0].QuestionId,
                    ExamId = result[0].ExamId,
                    QuestionTitle = result[0].QuestionTitle
                };
                for(int i=0; i < result.Count;i++)
                {
                    mcq.choices[i].ChoiceId = result[i].ChoiceId;
                    mcq.choices[i].ChoiceText = result[i].ChoiceText;
                    mcq.choices[i].IsCorrect = result[i].IsCorrect;
                    mcq.choices[i].QuestionId = result[i].QuestionId;
                }
                
                return View(mcq);
            }
            mcq.question.ExamId = Convert.ToInt32(examId);
            return View(mcq);
        }
        public IActionResult List(int? ExamId)
        {
            ViewBag.examId = Convert.ToInt32(ExamId);
            var QuestionsWithChoices = _ClsChoice.GetChoicesByExamId(Convert.ToInt32(ExamId));
            return View(QuestionsWithChoices);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MCQ mcq,string correctChoice ="0")
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", mcq);
            }
            if(!String.IsNullOrEmpty(correctChoice))
            {
                mcq.choices[Convert.ToInt32(correctChoice)].IsCorrect = true;
            }

            bool result = _ClsQuestion.Save(mcq.question, mcq.choices);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List", "Question", new { area = "Admin", ExamId = mcq.question.ExamId });
        }
        public IActionResult Delete(int id,int examId)
        {
            bool result = _ClsQuestion.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List", "Question", new { area = "Admin" ,ExamId = examId });
        }
    }
}
