using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }
        public int Age { get; set; }
        public string AboutMe { get; set; }
    }
}
