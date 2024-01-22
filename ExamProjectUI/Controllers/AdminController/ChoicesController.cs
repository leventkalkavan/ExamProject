using BusinessLayer.Concretes;
using BusinessLayer.DTOs.ChoiceDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamProjectUI.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class ChoicesController : Controller
    {
        private readonly IQuestionManager _questionManager;
        private readonly IChoiceManager _choiceManager;

        public ChoicesController(IQuestionManager questionManager, IChoiceManager choiceManager)
        {
            _questionManager = questionManager;
            _choiceManager = choiceManager;
        }

        [HttpGet]
        public IActionResult GetAllChoices()
        {
            var choices = _choiceManager.GetAll().Include(c => c.Question).ToList();

            var choiceDtos = choices.Select(choice => new ResultChoiceDto
            {
                Id = choice.Id.ToString(),
                Text = choice.Text,
                TrueChoice = choice.TrueChoice,
                QuestionName = choice.Question.Description
            }).ToList();

            return View(choiceDtos);
        }

        [HttpGet]
        public IActionResult CreateChoice()
        {
            var questions = _questionManager.GetAll().ToList();
            ViewBag.Questions = new SelectList(questions, "Id", "Description");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChoice(CreateChoiceDto dto)
        {
            if (ModelState.IsValid)
            {
                var newChoice = new Choice
                {
                    Text = dto.Text,
                    TrueChoice = dto.TrueChoice,
                    QuestionId = dto.QuestionId
                };

                await _choiceManager.AddAsync(newChoice);
                await _choiceManager.SaveAsync();

                return RedirectToAction("GetAllChoices");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChoice(string id)
        {
            var choice = await _choiceManager.GetByIdAsync(id);

            if (choice == null)
            {
                return NotFound();
            }

            var questions = _questionManager.GetAll().ToList();
            ViewBag.Questions = new SelectList(questions, "Id", "Description");


            var choiceDto = new UpdateChoiceDto()
            {
                Id = choice.Id,
                Text = choice.Text,
                QuestionId = choice.QuestionId,
                TrueChoice = choice.TrueChoice,
            };

            return View(choiceDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChoice(UpdateChoiceDto dto)
        {
            if (ModelState.IsValid)
            {
                var choiceToUpdate = await _choiceManager.GetByIdAsync(dto.Id.ToString());

                if (choiceToUpdate == null)
                {
                    return NotFound();
                }

                choiceToUpdate.Text = dto.Text;
                choiceToUpdate.TrueChoice = dto.TrueChoice;
                choiceToUpdate.QuestionId = dto.QuestionId;

                _choiceManager.Update(choiceToUpdate);
                await _choiceManager.SaveAsync();

                return RedirectToAction("GetAllChoices");
            }

            var questions = _questionManager.GetAll().ToList();
            ViewBag.Questions = new SelectList(questions, "Id", "Description");

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _choiceManager.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            await _choiceManager.RemoveAsync(id);
            await _choiceManager.SaveAsync();

            return RedirectToAction("GetAllChoices");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteChoice(string id)
        {
            var exam = await _choiceManager.GetByIdAsync(id);
            if (exam != null)
            {
                await _choiceManager.RemoveAsync(id);
                await _choiceManager.SaveAsync();
            }

            return RedirectToAction("Delete");
        }
    }
}