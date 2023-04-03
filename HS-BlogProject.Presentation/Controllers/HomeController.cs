using HS_BlogProject.Application.Models.VMs.PostVMs;
using HS_BlogProject.Application.Services.PostService;
using HS_BlogProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HS_BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }



        public async Task<IActionResult> Index()
        {
            // postları görüntüleme sayfası
            // model postları iletecek View'a

            List<PostVM> posts = await _postService.GetPosts();
           

            return View(posts);
        }
    }
}