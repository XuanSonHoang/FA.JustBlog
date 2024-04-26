using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> Register(RegisterModel register);
        Task<bool> Login(LoginModel login);
    }
}
