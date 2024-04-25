using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FA.JustBlog.Services;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;
using FA.JustBlog.Helpers;

namespace FA.JustBlog.Controllers {
    public class PostController : Controller {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LoggerHelper _loggerHelper;

        public PostController(
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            UserManager<ApplicationUser> userManager,
            LoggerHelper loggerHelper) {
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            _userManager = userManager;
            _loggerHelper = loggerHelper;
        }
        [HttpGet]
        public IActionResult Index(int? page, int? pageSize, List<Post>? searchList) {
            try {
                const int defaultPageSize = 5;
                const int defaultPageNumber = 1;

                _loggerHelper.LogDebug("Index action method started.");

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

                List<Tag> tags = _tagRepository.GetAllTags().Take(5).ToList();
                ViewBag.Tags = tags;
                ViewBag.posts = posts;
                List<Post> top5Posts = _postRepository.GetPostByViewCount(5);
                ViewBag.posts5 = top5Posts;
                List<Post> top5Lastest = _postRepository.GetLatestPost(5).ToList();
                ViewBag.PostsLastest = top5Lastest;
                List<Category> listCates = _categoryRepository.GetAllCategories().ToList();
                ViewBag.Categories = listCates;

                _loggerHelper.LogInformation($"Fetched {posts.Count} posts.");

                _loggerHelper.LogDebug("Fetching top 5 tags.");
                tags = _tagRepository.GetAllTags().Take(5).ToList();
                _loggerHelper.LogInformation($"Fetched {tags.Count} tags.");

                _loggerHelper.LogDebug("Fetching top 5 posts by view count.");
                top5Posts = _postRepository.GetPostByViewCount(5);
                _loggerHelper.LogInformation($"Fetched {top5Posts.Count} top posts by view count.");

                _loggerHelper.LogDebug("Fetching latest 5 posts.");
                top5Lastest = _postRepository.GetLatestPost(5).ToList();
                _loggerHelper.LogInformation($"Fetched {top5Lastest.Count} latest posts.");

                _loggerHelper.LogDebug("Fetching all categories.");
                listCates = _categoryRepository.GetAllCategories().ToList();
                _loggerHelper.LogInformation($"Fetched {listCates.Count} categories.");

                _loggerHelper.LogInformation("Index action method completed successfully.");

                return View(posts);
            } catch (Exception ex) {
                _loggerHelper.LogError($"Error in Index action method: {ex.Message}");
                _loggerHelper.LogDebug($"Error details: {ex.StackTrace}");

                // Handle the error (e.g., return an error view or redirect to a custom error page)
                return View("Error");  // Assuming you have an "Error" view
            }
        }


        [HttpGet]
        public IActionResult PostIncludeCates(int CateId) {
            List<Category> listCates = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = listCates;
            List<Post> listPostHaveCates = _postRepository.GetPostsByCategory(CateId).ToList();
            ViewBag.Posts = listPostHaveCates;
            ViewData["CateName"] = _categoryRepository.Find(CateId).Name;
            return View();
        }
        public PartialViewResult MostViewedPosts() {
            List<Post> top5Posts = _postRepository.GetPostByViewCount(5);
            return PartialView("_ListPosts", top5Posts);
        }

        public IActionResult HighestViewCount() {
            List<Post> top5Posts = _postRepository.GetPostByViewCount(5);
            ViewBag.posts = top5Posts;
            return View(ViewBag.posts);
        }

        public PartialViewResult LatestPostsinside() {
            List<Post> top5Lastest = _postRepository.GetLatestPost(5).ToList();
            return PartialView("_ListPosts", top5Lastest);
        }

        /*public IActionResult Details(int year, int month, string title)
        {
            var post = _postRepository.FindPost(year, month, title);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }*/
        public IActionResult Details(int id) {
            if (id == null) {
                return NotFound();
            }
            List<Category> listCates = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = listCates;
            List<Comment> comments = _commentRepository.GetCommentsForPost(id).ToList();
            ViewBag.Comments = comments;
            ViewBag.Email = _userManager.GetEmailAsync(_userManager.GetUserAsync(User).Result).Result.ToString();
            Post post = _postRepository.FindPost(id);
            if (post == null) {
                return NotFound();
            }
            return View(post);
        }
        [HttpGet]
        public IActionResult Search(string? title, string? shortDescription) {
            List<Category> listCates = _categoryRepository.GetAllCategories().ToList();
            ViewBag.Categories = listCates;
            List<Post> listSearch = _postRepository.SeachPostByTileAndShortDes(title, shortDescription).ToList();
            return View(listSearch);
        }
        public IActionResult ChangeLanguage(ChangeLanguageViewModel request) {
            if (request.IsSubmit) {
                HttpContext myContext = this.HttpContext;
                myContext.Response.Cookies.Append("Language", request.SelectedLanguage);
                return RedirectToAction("Index");
            }
            ChangeLanguage_PreparePresentation(request);
            return RedirectToAction("Index");
        }

        private void ChangeLanguage_PreparePresentation(ChangeLanguageViewModel request) {
            request.ListOfLanguages = new List<SelectListItem>
                        {
                            new SelectListItem
                            {
                                Text = "English",
                                Value = "en"
                            },

                            new SelectListItem
                            {
                                Text = "VietNamese",
                                Value = "vn",
                            }
                        };
        }
    }
}
