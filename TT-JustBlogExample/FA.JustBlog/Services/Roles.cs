using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace FA.JustBlog.Services
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string BlogOwner = "BlogOwner";
        public const string Contribute = "Contributor";
    }
}
