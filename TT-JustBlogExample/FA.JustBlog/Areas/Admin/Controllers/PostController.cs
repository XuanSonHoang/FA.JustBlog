using FA.JustBlog.Areas.Admin.Data.IRepositories;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Services ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FA.JustBlog.Core.IRepositories.ICategoryRepository _categoryRepository;
        public PostController(IPostRepository postRepository, UserManager<ApplicationUser> userManager, FA.JustBlog.Core.IRepositories.ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult PostsAction()
        {
            ViewBag.LastestPost = _postRepository.GetLatestPost(1).ToList();
            ViewBag.MostViewedPost = _postRepository.MostViewedPosts(1).ToList();
            ViewBag.MostInterestingPost = _postRepository.GetMostInterestingPosts(1).ToList();
            ViewBag.PublishedPost = _postRepository.GetPublishedPost(1).ToList();
            ViewBag.UnPublishedPost = _postRepository.GetUn_PublishedPost(1).ToList();
            return View();
        }

        [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Contribute)]
        public IActionResult Create()
        {
            List<Category> categories = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [Route("/Admin/Post/List/{page?}/{pageSize?}")]
        [HttpGet]
        public async Task<IActionResult> List(int? page, int? pageSize)
        {
            const int defaultPageSize = 5;
            const int defaultPageNumber = 1;

            var posts = _postRepository.GetAllPosts().ToList();

            page = page ?? defaultPageNumber;
            pageSize = pageSize ?? defaultPageSize;

            ViewBag.CurrentPageSize = pageSize;

            ViewBag.PageSize = new int[5] { 5, 10, 15, 20, 25 };
            var totalItems = posts.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize.Value);


            posts = posts.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

            ViewBag.SelectedPageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(posts);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Create(Post post)
        {
            try
            {
                Post newPost = new Post
                {
                    Title = post.Title,
                    ShortDescription = post.ShortDescription,
                    PostContent = post.PostContent,
                    UrlSlug = post.UrlSlug,
                    Published = post.Published,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    CategoryId = post.CategoryId,
                    ViewCount = 0,
                    RateCount = 0,
                    TotalRate = 0,
                    Rate = 0
                };

                _postRepository.InsertPost(newPost);
                return Redirect("List");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(post);
            }
        }
        [Authorize(Roles = Roles.BlogOwner)]
        public IActionResult DeletePost(int id)
        {
            var post = _postRepository.FindPost(id);
            if (post == null)
            {
                return NotFound();
            }
            _postRepository.DeletePost(post);

            return RedirectToRoute("Admin_AllList");
        }
        [Authorize(Roles = Roles.BlogOwner)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var post = _postRepository.FindPost(id);
            List<Category> categories = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = categories;
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        [Authorize(Roles = Roles.BlogOwner)]
        public IActionResult Update(Post post)
        {
            _postRepository.UpdatePost(post);
            return RedirectToRoute("Admin_AllList");            
        }

        public JsonResult ListUsingJqGrid()
        {
            List<Post> posts = _postRepository.GetAllPosts().ToList();
            return Json(new { rows = posts, total = 1, page = 1, records = posts.Count });
            //return Json(posts);
        }


    }
}
