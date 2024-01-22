using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concretes;
using BusinessLayer.DTOs.ExamAssignmentDtos;
using BusinessLayer.DTOs.ExamDtos;
using EntityLayer.Entities;
using EntityLayer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamProjectUI.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class ExamsController : Controller
    {
        private readonly IExamAssignmentManager _examAssignmentManager;
        private readonly IExamManager _examManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryManager _categoryManager;

        public ExamsController(IExamAssignmentManager examAssignmentManager, IExamManager examManager,
            ICategoryManager categoryManager, UserManager<AppUser> userManager)
        {
            _examAssignmentManager = examAssignmentManager;
            _examManager = examManager;
            _categoryManager = categoryManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllListExam()
        {
            var exams = _examManager.GetAll();
            var examsDto = exams.Select(exam => new ResultExamDto()
            {
                Id = exam.Id.ToString(),
                Name = exam.Name,
                ExamMinute = exam.ExamMinute,
                SuccessScore = exam.SuccessScore,
                CategoryName = exam.Category.Name,
                Description = exam.Description,
            }).ToList();
            return View(examsDto);
        }

        public IActionResult CreateExam()
        {
            var categories = _categoryManager.GetAll().ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamDto dto)
        {
            var categories = _categoryManager.GetAll().ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            if (ModelState.IsValid)
            {
                var value = new Exam()
                {
                    Name = dto.Name,
                    ExamMinute = dto.ExamMinute,
                    Description = dto.Description,
                    SuccessScore = dto.SuccessScore,
                    CategoryId = dto.CategoryId,
                };

                await _examManager.AddAsync(value);
                await _examManager.SaveAsync();

                return RedirectToAction("GetAllListExam");
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateExam(string id)
        {
            var exam = await _examManager.GetByIdAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            var categories = _categoryManager.GetAll().ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var examDto = new UpdateExamDto()
            {
                Id = exam.Id.ToString(),
                Name = exam.Name,
                ExamMinute = exam.ExamMinute,
                Description = exam.Description,
                SuccessScore = exam.SuccessScore,
                CategoryId = exam.CategoryId
            };

            return View(examDto);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateExam(UpdateExamDto dto)
        {
            if (ModelState.IsValid)
            {
                var exam = await _examManager.GetByIdAsync(dto.Id);

                if (exam != null)
                {
                    exam.Name = dto.Name;
                    exam.ExamMinute = dto.ExamMinute;
                    exam.Description = dto.Description;
                    exam.SuccessScore = dto.SuccessScore;
                    exam.CategoryId = dto.CategoryId;
                    exam.UpdatedDate = DateTime.Now;
                    _examManager.Update(exam);
                    await _examManager.SaveAsync();

                    return RedirectToAction("GetAllListExam");
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

            var exam = await _examManager.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            await _examManager.RemoveAsync(id);
            await _examManager.SaveAsync();

            return RedirectToAction("GetAllListExam");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var exam = await _examManager.GetByIdAsync(id);
            if (exam != null)
            {
                await _examManager.RemoveAsync(id);
                await _examManager.SaveAsync();
            }

            return RedirectToAction("Delete");
        }

        //sinav atama islemleri


        [HttpGet]
        public IActionResult GetAllListExamAssignments()
        {
            var examAssignments = _examAssignmentManager.GetAll();
            var examAssignmentsDto = examAssignments.Select(ea => new ResultExamAssignmentDto()
            {
                Id = ea.Id.ToString(),
                ExamName = ea.Exam.Name,
                StartTime = ea.StartTime.ToString("dd-MM-yyyy HH:mm"),
                EndTime = ea.EndTime.ToString("dd-MM-yyyy HH:mm")
            }).ToList();
            return View(examAssignmentsDto);
        }

        [HttpGet]
        public IActionResult CreateExamAssignment()
        {
            var exams = _examManager.GetAll().ToList();
            var users = _userManager.Users.ToList();
            var defaultUserOption = new SelectListItem { Value = null, Text = "Seçiniz", Selected = true };
            var userSelectList = new List<SelectListItem> { defaultUserOption };
            userSelectList.AddRange(
                users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName }));

            ViewBag.Exams = new SelectList(exams, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateExamAssignment(CreateExamAssignmentDto dto)
        {
            if (ModelState.IsValid)
            {
                var examAssignment = new ExamAssignment()
                {
                    ExamId = dto.ExamId,
                    UserId = dto.UserId,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime
                };

                await _examAssignmentManager.AddAsync(examAssignment);
                await _examAssignmentManager.SaveAsync();
                return RedirectToAction("GetAllListExamAssignments");
            }

            var exams = _examManager.GetAll().ToList();
            var users = _userManager.Users.ToList();
            ViewBag.Exams = new SelectList(exams, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            return View(dto);
        }
    }
}