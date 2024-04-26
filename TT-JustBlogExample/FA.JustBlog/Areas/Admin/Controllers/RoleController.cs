using FA.JustBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.BlogOwner + ", " + Roles.Admin)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Route("Admin/Role/List")]
        public IActionResult List()
        {   
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }
        [Route("Admin/Role/Create")]
        public IActionResult Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                if(_roleManager.RoleExistsAsync(roleName).Result == false)
                {
                    var role = new IdentityRole(roleName);
                    var result = _roleManager.CreateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("List");
                    }
                }
            }
            return View();
        }
        [Route("Admin/Role/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
            }
            return NotFound();
        }
        [HttpPost]
        [Route("Admin/Role/Edit/{id}")]
        public IActionResult Edit(string id, string roleName)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                role.Name = roleName;
                return View();
            }
            return NotFound();
        }
    }
}
