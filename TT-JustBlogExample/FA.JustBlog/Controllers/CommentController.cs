using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FA.JustBlog.Services;

namespace FA.JustBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, UserManager<ApplicationUser> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
            _commentRepository = commentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComment(string content, int postId)
        {
            if(content == null)
            {
                return RedirectToAction("Index");
            }
            Comment comment = new Comment()
            {
                Name = _userManager.GetUserNameAsync(_userManager.GetUserAsync(User).Result).Result,
                Email = _userManager.GetEmailAsync(_userManager.GetUserAsync(User).Result).Result,
                CommentText = content,
                CommentHeader = "Comment Header",
                CommentTime = System.DateTime.Now,
                PostId = postId
            };
            int id = postId;
            _commentRepository.AddComment(comment);
            return RedirectToAction("Details", "Post", new { id });
        }
        [Authorize(Roles = Roles.BlogOwner)]
        public IActionResult DeleteComment(int idC, int postId)
        {
            _commentRepository.DeleteComment(idC);

            int id = postId;
            
            return RedirectToAction("Details", "Post", new { id });
        }
        [HttpGet]
        public JsonResult GetAllComment(int id)
        {
            var commentList = _commentRepository.GetCommentsForPost(id);
            return Json(commentList);
        }

    }
}
