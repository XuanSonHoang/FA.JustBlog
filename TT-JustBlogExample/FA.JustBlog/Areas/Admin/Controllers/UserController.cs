using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Authorization;
using FA.JustBlog.Services;
using System.Drawing;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }   
        [Route("/Admin/User/List")]
        public IActionResult List()
        {
            var users = _userManager.Users.ToList();
            List<UserWithRolesViewModel> usersWithRoles = new List<UserWithRolesViewModel>();

            // Take all roles
            var roles = _roleManager.Roles.ToList();
            ViewBag.Count = roles.Count;
            string[] rolesName = new string[roles.Count];
            for (int i = 0; i < roles.Count; i++)
            {
                rolesName[i] = roles[i].Name;
            }
            ViewBag.Roles = rolesName;

            foreach (var user in users)
            {   
                var userRoles = _userManager.GetRolesAsync(user).Result;
                usersWithRoles.Add(new UserWithRolesViewModel
                {
                    User = user,
                    Roles = userRoles
                }) ;
            }

            return View(usersWithRoles);
        }
        [HttpPost]
        [Route("/Admin/User/ActionRole")]
        public async Task<IActionResult> actionRole(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userHasRole = await _userManager.IsInRoleAsync(user, roleName);
            if (userHasRole)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return RedirectToAction("List");
                }
            }
            else
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("List");
        }
        public IActionResult ManageRoles()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.roles = roles;
            return View();
        }
    }
}
