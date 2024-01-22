using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concretes;
using BusinessLayer.DTOs.QuestionCategoryDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamProjectUI.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class QuestionCategoriesController : Controller
    {
        private readonly IQuestionCategoryManager _questionCategoryManager;
        private readonly IExamManager _examManager;

        public QuestionCategoriesController(IQuestionCategoryManager questionCategoryManager, IExamManager examManager)
        {
            _questionCategoryManager = questionCategoryManager;
            _examManager = examManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllListQuestionCategories()
        {
            var questionCategories = _questionCategoryManager.GetAll();
            var quesitonCategoriesDto = questionCategories.Select(qc => new ResultQuestionCategoryDto()
            {
                Id = qc.Id.ToString(),
                Name = qc.Name,
                ExamName = qc.Exam.Name
            }).ToList();
            return View(quesitonCategoriesDto);
        }

        public IActionResult CreateQuestionCategory()
        {
            var exam = _examManager.GetAll().ToList();
            ViewBag.Exams = new SelectList(exam, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestionCategory(CreateQuestionCategoryDto dto)
        {
            var exam = _examManager.GetAll().ToList();
            ViewBag.Exams = new SelectList(exam, "Id", "Name");
            if (ModelState.IsValid)
            {
                var value = new QuestionCategory()
                {
                    Name = dto.Name,
                    ExamId = dto.ExamId
                };
                await _questionCategoryManager.AddAsync(value);
                await _questionCategoryManager.SaveAsync();
                return RedirectToAction("GetAllListQuestionCategories");
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateQuestionCategory(string id)
        {
            var questionCategory = await _questionCategoryManager.GetByIdAsync(id);

            if (questionCategory == null)
            {
                return NotFound();
            }

            var exams = _examManager.GetAll().ToList();
            ViewBag.Exams = new SelectList(exams, "Id", "Name");

            var qcDto = new UpdateQuestionCategoryDto()
            {
                Id = questionCategory.Id.ToString(),
                Name = questionCategory.Name,
                ExamId = questionCategory.ExamId
            };
            return View(qcDto);
        }
 
        
        [HttpPost]
        public async Task<IActionResult> UpdateQuestionCategory(UpdateQuestionCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var questionCategory = await _questionCategoryManager.GetByIdAsync(dto.Id);

                if (questionCategory != null)
                {
                    questionCategory.Name = dto.Name;
                    questionCategory.ExamId = dto.ExamId;
                    questionCategory.UpdatedDate = DateTime.Now;
                    _questionCategoryManager.Update(questionCategory);
                    await _questionCategoryManager.SaveAsync();

                    return RedirectToAction("GetAllListQuestionCategories");
                }
            }

            return View(dto);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _questionCategoryManager.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            await _questionCategoryManager.RemoveAsync(id);
            await _questionCategoryManager.SaveAsync();

            return RedirectToAction("GetAllListQuestionCategories");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteQuestionCategories(string id)
        {
            var exam = await _questionCategoryManager.GetByIdAsync(id);
            if (exam != null)
            {
                await _questionCategoryManager.RemoveAsync(id);
                await _questionCategoryManager.SaveAsync();
            }

            return RedirectToAction("Delete");
        }
    }
}