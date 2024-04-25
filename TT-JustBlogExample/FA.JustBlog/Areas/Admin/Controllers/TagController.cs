using FA.JustBlog.Core.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [Route("/Admin/Tag/List")]
        public IActionResult List()
        {
            ViewBag.Tags = _tagRepository.GetAllTags();
            return View();
        }

    }
}
