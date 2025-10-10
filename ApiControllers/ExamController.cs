using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.BL;
using OnlineExamSystem.Classes;
using OnlineExamSystem.Models;
using System.Text.Json;

namespace OnlineExamSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        IChoice _ClsChoice;
        ISubmission _ClsSubmission;
        UserManager<ApplicationUser> _userManager;
        public ExamController(IChoice Clsoice, ISubmission clsSubmission, UserManager<ApplicationUser> userManager)
        {
            _ClsChoice = Clsoice;
            _ClsSubmission = clsSubmission;
            _userManager = userManager;
        }

        private const string UserAnswersKey = "UserAnswers";
        private const string CorrectAnswersKey = "CorrectAnswers";

        // Initialize exam data
        private ExamData GetExamDataDB(int id)
        {
            var lstMcq = _ClsChoice.GetChoicesByExamId(id);
            ExamData examData = new ExamData(); 
            
            for (int i = 3;i< lstMcq.Count;i+=4)
            {
                QuestionAnswer questionAnswer = new QuestionAnswer();
                string correctAnswerId = "";

                questionAnswer.Id = lstMcq[i].QuestionId;
                questionAnswer.Text = lstMcq[i].QuestionTitle;
                for (int j = i - 3; j <= i; j++)
                {
                    Option optio = new Option();
                    optio.Id = (lstMcq[j].ChoiceId).ToString();
                    optio.Text = lstMcq[j].ChoiceText;
                    if (lstMcq[j].IsCorrect == true)
                        correctAnswerId = lstMcq[j].ChoiceId.ToString();
                    questionAnswer.Options.Add(optio);
                }
                
                questionAnswer.CorrectAnswerId = correctAnswerId;

                examData.QuestionsAnswers.Add(questionAnswer);
            }
            return examData;
        }

        // Helper methods for session management 
        private void SetSessionObject(string key, object value)
        {
            HttpContext.Session.SetString(key, JsonSerializer.Serialize(value));
        }

        private T GetSessionObject<T>(string key)
        {
            var value = HttpContext.Session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }

        // Get exam data
        [HttpGet("{id}")]
        public IActionResult GetExamData(int id)
        {
            var examData = GetExamDataDB(id);

            // Store correct answers in session
            var correctAnswers = examData.QuestionsAnswers.Select(q => q.CorrectAnswerId).ToList();
            SetSessionObject(CorrectAnswersKey, correctAnswers);

            // Initialize user answers session
            SetSessionObject(UserAnswersKey, new List<string>());

            return Ok(examData);
        }

        // Save user answer
        [HttpPost]
        [Route("SaveAnswer")]
        public IActionResult SaveAnswer([FromBody] AnswerRequest request)
        {
            try
            {
                var userAnswers = GetSessionObject<List<string>>(UserAnswersKey) ?? new List<string>();
                userAnswers.Add(request.ChoiceId);
                SetSessionObject(UserAnswersKey, userAnswers);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        // Submit exam and calculate results and Save it in DataBase
        [HttpGet("SubmitExam/{id}")]
        [Route("~/api/Exame/SubmitExam/{id}")]
        public async Task<IActionResult> SubmitExam(int id)
        {
            try
            {
                var userAnswers = GetSessionObject<List<string>>(UserAnswersKey) ?? new List<string>();
                var correctAnswers = GetSessionObject<List<string>>(CorrectAnswersKey) ?? new List<string>();

                // Calculate score
                int correctCount = 0;
                for (int i = 0; i < correctAnswers.Count && i < userAnswers.Count; i++)
                {
                    if (userAnswers[i] == correctAnswers[i])
                    {
                        correctCount++;
                    }
                }

                double score = (double)correctCount / correctAnswers.Count * 100;
                bool passed = score >= 60;

                var user = await _userManager.GetUserAsync(User);
                var result = new submission
                {
                    UserId = user.Id,
                    ExamId = id,
                    Score = (float)Math.Round(score, 2),
                    Status = passed.ToString()
                };
                bool isDone =  _ClsSubmission.Save(result);
                if (!isDone)
                    return BadRequest(new { success = false, error = "Saving Result Error" });

                return Ok(new
                {
                    success = true,
                    score = Math.Round(score, 2),
                    passed = passed,
                    message = "Exam submitted successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        [Route("ResetExam")]
        public IActionResult ResetExam()
        {
            SetSessionObject(UserAnswersKey, new List<string>());
            return Ok(new { success = true });
        }
    }
}

