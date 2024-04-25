using FA.JustBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    public class CaptchaController : Controller
    {
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            //generate captcha code
            var captchaCode = Captcha.GenerateCaptchaCode();
            //generate captcha image
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            //add session to store captcha code
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            //return captcha image
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
    }
}
