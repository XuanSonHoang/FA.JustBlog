using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace FA.JustBlog.Models
{
    public class UserWithRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
