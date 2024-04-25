using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Areas.Identity.Models
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
