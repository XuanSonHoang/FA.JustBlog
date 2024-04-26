using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/Identity/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJustBlogContext justBlogContext;
        private readonly ILogger<RoleController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, IJustBlogContext justBlogContext, ILogger<RoleController> logger, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            this.justBlogContext = justBlogContext; 
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage)
        {
            List<ApplicationUser> model = _userManager.Users.ToList();

          

            return View(model);
        }
    }
}
