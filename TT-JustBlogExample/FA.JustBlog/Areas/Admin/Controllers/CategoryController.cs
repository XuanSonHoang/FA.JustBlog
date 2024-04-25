using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [Route("/Admin/Category/List")]
        public IActionResult List()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(_categoryRepository.GetAllCategories());
        }
        [HttpGet]
        [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Contribute)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("/Admin/Category/Create")]
        [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Contribute)]
        public async Task<IActionResult> Create(Category cate)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddCategoryAsync(cate);
                return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Error");
            return View(cate);
        }
        [HttpGet]
        [Authorize(Roles = "BlogOwner")]

        public async Task<IActionResult> Delete(int id)
        {
            Category cate = _categoryRepository.Find(id);
            if (cate == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategoryAsync(cate);
            return RedirectToAction("List");
        }
        [HttpGet]
        [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Contribute)]
        public IActionResult Update(int id)
        {
            Category cate = _categoryRepository.Find(id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }
        [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Contribute)]
        [HttpPost]
        public async Task<IActionResult> Update(Category cate)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(cate);
                return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Error when update");
            return View(cate);
        }
    }
}
