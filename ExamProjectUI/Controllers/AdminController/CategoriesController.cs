using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concretes;
using BusinessLayer.DTOs.CategoryDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamProjectUI.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult GetAllListCategory()
        {
            var categories = _categoryManager.GetAll().ToList();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = dto.Name
                };

                await _categoryManager.AddAsync(category);
                await _categoryManager.SaveAsync();

                return RedirectToAction("GetAllListCategory");
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var category = await _categoryManager.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new UpdateCategoryDto
            {
                Id = category.Id.ToString(),
                Name = category.Name
            };

            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryManager.GetByIdAsync(dto.Id.ToString());

                if (category != null)
                {
                    category.Name = dto.Name;
                    category.UpdatedDate = DateTime.Now;

                    _categoryManager.Update(category);
                    await _categoryManager.SaveAsync();

                    return RedirectToAction("GetAllListCategory");
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

            var category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryManager.RemoveAsync(id);
            await _categoryManager.SaveAsync();

            return RedirectToAction("GetAllListCategory");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryManager.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryManager.RemoveAsync(id);
                await _categoryManager.SaveAsync();
            }

            return RedirectToAction("GetAllListCategory");
        }
    }
}