using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly <ApplicationUser> _signInManager;
        public AccountRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration/*, SignInManager<ApplicationUser> signInManager*/)
        {
            _configuration = configuration;
            _userManager = userManager;
            //_signInManager = signInManager;
        }
        public async Task<bool> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return true;
            }
            return false;
        }

        public async Task<IdentityResult> Register(RegisterModel register)
        {
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Age = register.Age,
                AboutMe = register.AboutMe
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            return result;
        }
    }
}
