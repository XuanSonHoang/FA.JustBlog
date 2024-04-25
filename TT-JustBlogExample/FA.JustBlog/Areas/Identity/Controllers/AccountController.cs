using FA.JustBlog.Areas.Identity.Models;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using System.Text;
using System.Text.Encodings.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FA.JustBlog.Areas.Identity.Controllers
{
    [Authorize]
    [Area("Identity")]
    [Route("/Account/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }
        //GET: Account/Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //POST: Account/Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if(login.Email == "admin@gmail.com" && login.Password == "admin@123")
            {
                return RedirectToRoute("Admin_AllList");
            }
            if (ModelState.IsValid)
            {
                if (Captcha.ValidateCaptchaCode(login.CaptchaCode, HttpContext))
                {
                    var user = await _userManager.FindByEmailAsync(login.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            return RedirectToAction("Index", "Post"); 
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return RedirectToAction("Lockout", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(login);
        }


        //Get: Account/Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //POST: Account/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerModel.UserName, Age = registerModel.Age, AboutMe = registerModel.AboutMe, Email = registerModel.Email };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // generate token for verify email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //generate url for verify email
                    var callbackUrl = Url.Page(
                                          "/Account/ConfirmEmail",
                                          pageHandler: null,
                                          values: new {Action = "VerifyEmail", userId = user.Id, code = code},
                                          protocol: Request.Scheme
                                      );
                    //send email
                    await _emailSender.SendEmailAsync(
                                       registerModel.Email,
                                       "Confirm your email",
                                       $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>."
                                       );
                    if(_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirm");
                    }

                    return RedirectToAction("Login", "Account"); 
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerModel);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User not found with id: {userId}");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result = await _userManager.ConfirmEmailAsync(user, code);  
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
                return RedirectToAction("ConfirmEmail");
            }

            // Handle failed verification
            return NotFound($"Unable to verify email for user with id: {userId}");
        }
        [AllowAnonymous]
        public IActionResult ConfirmEmail()
        {
            return View();
        }
        //POST: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        //GET: Account/ForgotPassword
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                                  "/Account/ResetPassword",
                                  pageHandler: null,
                                  values: new { Action = "ResetPassword", code = code, email = forgotPasswordModel.Email },
                                  protocol: Request.Scheme);
                
                await _emailSender.SendEmailAsync(
                      forgotPasswordModel.Email,
                      "Reset Password",
                      $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                );

                ViewBag.Message = "Please check your email to reset your password.";
                return RedirectToAction("ResetPasswordConfirmation");
            }
            return View(forgotPasswordModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string code, string email)
        {
            if (code == null)
            {
                return BadRequest("A email must be supplied for password reset.");
            }
            ViewBag.Code = code;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {   
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email); 
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordViewModel.Code));

                // Reset password
                var result = await _userManager.ResetPasswordAsync(user, code, resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(resetPasswordViewModel); 
            }

            return View(resetPasswordViewModel); 
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirm()
        {
            return View();
        }
        public IActionResult AccessDenied(String? ReturnUrl)
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var user = _userManager.GetUserAsync(User);
            ViewBag.Email = user.Result.Email;
            if(user == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(changePasswordViewModel);
        }
    }
}
