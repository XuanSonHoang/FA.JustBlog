using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;

namespace FA.JustBlog.Controllers
{
    public class TagController : Controller
    {
        private ITagRepository _context;
        private ICategoryRepository _categoryRepository;
        public TagController(ITagRepository context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult PopularTag()
        {
            //code to get tag desc by count
            List<Tag> tagList = _context.GetAllTags()
                            .OrderByDescending(p => p.Count)
                            .Take(10)
                            .ToList();

            return PartialView("_PopularTags", tagList);
        }
        [Authorize(Roles = "Contributor")]
        public IActionResult PopularTags()
        {
            List<Category> listCates = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = listCates;
            List<Tag> tagList = _context.GetAllTags()
                            .OrderByDescending(p => p.Count)
                            .Take(10)
                            .ToList();
            ViewBag.tags = tagList;
            return View();
        }
    }
}
