﻿using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
