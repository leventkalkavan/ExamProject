using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concretes;
using BusinessLayer.DTOs.QuestionDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamProjectUI.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionManager _questionManager;
        private readonly IQuestionCategoryManager _questionCategoryManager;

        public QuestionsController(IQuestionManager questionManager, IQuestionCategoryManager questionCategoryManager)
        {
            _questionManager = questionManager;
            _questionCategoryManager = questionCategoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllListQuestions()
        {
            var questions = _questionManager.GetAll();
            var questionDto = questions.Select(question => new ResultQuestionDto()
            {
                Id = question.Id.ToString(),
                Description = question.Description,
                QuestionType = question.QuestionType,
                QuestionCategoryName = question.QuestionCategory.Name ?? "N/A"
            }).ToList();

            return View(questionDto);
        }

        public IActionResult CreateQuestion()
        {
            var questionCategories = _questionCategoryManager.GetAll().ToList();
            var questionTypes = Enum.GetValues(typeof(QuestionType))
                .Cast<QuestionType>()
                .Select(x => new SelectListItem
                {
                    Text = x == QuestionType.MultipleChoice ? "Çoktan Seçmeli" : "Açıklamalı",
                    Value = ((int)x).ToString()
                });

            ViewBag.QuestionCategories = new SelectList(questionCategories, "Id", "Name");
            ViewBag.QuestionTypes = questionTypes;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
        {
            var value = new Question()
            {
                Description = dto.Description,
                QuestionType = dto.QuestionType,
                QuestionCategoryId = dto.QuestionCategoryId
            };

            await _questionManager.AddAsync(value);
            await _questionManager.SaveAsync();
            return RedirectToAction("GetAllListQuestions");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateQuestion(string id)
        {
            var question = await _questionManager.GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var qCategories = _questionCategoryManager.GetAll().ToList();
            var questionTypes = Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>()
                .Select(x => new SelectListItem
                {
                    Text = GetQuestionTypeDisplayText(x),
                    Value = ((int)x).ToString()
                });

            ViewBag.QuestionCategory = new SelectList(qCategories, "Id", "Name");
            ViewBag.QuestionTypes = questionTypes;

            var qDto = new UpdateQuestionDto()
            {
                Id = question.Id.ToString(),
                Description = question.Description,
                QuestionType = question.QuestionType
            };

            return View(qDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionDto dto)
        {
            if (ModelState.IsValid)
            {
                var question = await _questionManager.GetByIdAsync(dto.Id);

                if (question != null)
                {
                    question.Description = dto.Description;
                    question.QuestionType = dto.QuestionType;
                    question.UpdatedDate = DateTime.Now;
                    _questionManager.Update(question);
                    await _questionCategoryManager.SaveAsync();
                    return RedirectToAction("GetAllListQuestions");
                }
            }

            var qCategories = _questionCategoryManager.GetAll().ToList();
            var questionTypes = Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>()
                .Select(x => new SelectListItem
                {
                    Text = GetQuestionTypeDisplayText(x),
                    Value = ((int)x).ToString()
                });

            ViewBag.QuestionCategory = new SelectList(qCategories, "Id", "Name");
            ViewBag.QuestionTypes = questionTypes;

            return View(dto);
        }

        private string GetQuestionTypeDisplayText(QuestionType questionType)
        {
            switch (questionType)
            {
                case QuestionType.MultipleChoice:
                    return "Çoktan Seçmeli";
                case QuestionType.Descriptive:
                    return "Açıklamalı";
                default:
                    return questionType.ToString();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _questionManager.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            await _questionManager.RemoveAsync(id);
            await _questionManager.SaveAsync();

            return RedirectToAction("GetAllListQuestions");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteQuesiton(string id)
        {
            var question = await _questionManager.GetByIdAsync(id);
            if (question != null)
            {
                await _questionManager.RemoveAsync(id);
                await _questionManager.SaveAsync();
            }

            return RedirectToAction("Delete");
        }
    }
}