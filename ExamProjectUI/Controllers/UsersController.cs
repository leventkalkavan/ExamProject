using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concretes;
using EntityLayer.Entities;
using EntityLayer.Entities.Identity;
using ExamProjectUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProjectUI.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IExamAssignmentManager _examAssignmentManager;
        private readonly IQuestionManager _questionManager;
        private readonly IChoiceManager _choiceManager;
        private readonly IExamAnswerManager _answerManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExamManager _examManager;

        public UsersController(IExamAssignmentManager examAssignmentManager, IExamAnswerManager answerManager,
            UserManager<AppUser> userManager, IQuestionManager questionManager, IChoiceManager choiceManager,
            IExamManager examManager)
        {
            _examAssignmentManager = examAssignmentManager;
            _answerManager = answerManager;
            _userManager = userManager;
            _questionManager = questionManager;
            _choiceManager = choiceManager;
            _examManager = examManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var examAssignments = _examAssignmentManager
                    .GetAll()
                    .Include(ea => ea.Exam)
                    .Include(ea => ea.UserExamAnswers)
                    .Where(ea => ea.UserId.ToString() == user.Id)
                    .ToList();

                return View(examAssignments);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> StartExam(int examAssignmentId)
        {
            var examAssignment = await _examAssignmentManager
                .GetAll()
                .Include(ea => ea.Exam)
                .FirstOrDefaultAsync(ea => ea.Id == examAssignmentId);

            if (examAssignment == null || examAssignment.Exam == null)
            {
                return RedirectToAction("Index", "Home");
            }

            bool hasSubmittedAnswers = _answerManager.GetAll()
                .Any(ea => ea.UserId == examAssignment.UserId && ea.ExamAssignmentId == examAssignment.Id);

            if (hasSubmittedAnswers)
            {
                return RedirectToAction("GetAllExams");
            }

            var model = new StartExamViewModel
            {
                ExamAssignmentId = examAssignmentId.ToString(),
                ExamName = examAssignment.Exam.Name,
                ExamDescription = examAssignment.Exam.Description,
                ExamMinute = examAssignment.Exam.ExamMinute,
                StartTime = examAssignment.StartTime,
                EndTime = examAssignment.EndTime
            };
            TempData["RemainingTime"] = model.ExamMinute * 60;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExamPage(string examAssignmentId)
        {
            var examAssignment = await _examAssignmentManager.GetByIdAsync(examAssignmentId);

            if (examAssignment == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var exam = await _examManager.GetByIdAsync(examAssignment.ExamId.ToString());
            if (exam == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var remainingTime = TimeSpan.FromSeconds(Convert.ToDouble(TempData["RemainingTime"]));

            var examQuestions = _questionManager
                .GetAll()
                .Include(q => q.Choices)
                .Where(q => q.QuestionCategoryId == examAssignment.ExamId)
                .ToList();

            var model = new ExamPageViewModel
            {
                ExamAssignmentId = examAssignmentId,
                Questions = examQuestions.Select(q => new QuestionViewModel
                {
                    QuestionId = q.Id,
                    Description = q.Description,
                    QuestionType = q.QuestionType,
                    Choices = q.Choices.Select(c => new ChoiceViewModel
                    {
                        ChoiceId = c.Id,
                        Text = c.Text
                    }).ToList()
                }).ToList(),
                RemainingTime = remainingTime
            };

            TempData.Remove("RemainingTime");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitExam(SubmitExamViewModel model)
        {
            var examAssignment = await _examAssignmentManager.GetByIdAsync(model.ExamAssignmentId.ToString());

            if (examAssignment == null)
            {
                return RedirectToAction("Index", "Home");
            }

            bool hasSubmittedAnswers = _answerManager.GetAll()
                .Any(ea => ea.UserId == examAssignment.UserId && ea.ExamAssignmentId == examAssignment.Id);

            if (hasSubmittedAnswers)
            {
                return RedirectToAction("GetAllExams");
            }

            int totalQuestions = model.QuestionAnswers.Count;
            int correctAnswers = 0;
            int score = 0;

            foreach (var answer in model.QuestionAnswers)
            {
                var question = await _questionManager.GetByIdAsync(answer.QuestionId.ToString());
                var choice = await _choiceManager.GetByIdAsync(answer.SelectedChoiceId.ToString());

                if (question != null && question.QuestionType == QuestionType.MultipleChoice)
                {
                    var selectedChoice = question.Choices?.FirstOrDefault(c => c.Id == answer.SelectedChoiceId);

                    if (selectedChoice != null && choice != null && choice.TrueChoice)
                    {
                        correctAnswers++;
                    }

                    int questionScore = (selectedChoice != null && choice.TrueChoice)
                        ? 100 / totalQuestions
                        : 0;

                    var userAnswer = new ExamAnswer
                    {
                        ExamId = examAssignment.ExamId,
                        UserId = examAssignment.UserId,
                        ExamAssignmentId = examAssignment.Id,
                        QuestionId = answer.QuestionId,
                        SelectedChoiceId = answer.SelectedChoiceId,
                        AnsweredAt = DateTime.UtcNow,
                        Score = questionScore
                    };

                    await _answerManager.AddAsync(userAnswer);
                }
            }

            if (totalQuestions > 0)
            {
                score = (int)Math.Round(((double)correctAnswers / totalQuestions) * 100);
            }

            await _answerManager.SaveAsync();

            return RedirectToAction("GetAllExams");
        }
    }
}