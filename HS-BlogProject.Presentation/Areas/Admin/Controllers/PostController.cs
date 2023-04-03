using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.DTOs.PostDTOs;
using HS_BlogProject.Application.Models.VMs.PostVMs;
using HS_BlogProject.Application.Services.AuthorService;
using HS_BlogProject.Application.Services.GenreService;
using HS_BlogProject.Application.Services.PostService;
using HS_BlogProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HS_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;


        public PostController(IPostService postService, IGenreService genreService, IAuthorService authorService)
        {
            _postService = postService;
            _genreService = genreService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            List<PostVM> posts = await _postService.GetPosts();

            return View(posts);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genre = new SelectList(createPostDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(createPostDTO.Authors, "Id", "FullName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO post)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(post);
                return RedirectToAction("Index");
            }
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genre = new SelectList(createPostDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(createPostDTO.Authors, "Id", "FullName");
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genre = new SelectList(createPostDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(createPostDTO.Authors, "Id", "FullName");
            return View(await _postService.GetByID(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO post)
        {

            if (ModelState.IsValid)
            {
               
                await _postService.Update(post);
                return RedirectToAction("Index");
            }
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genre = new SelectList(createPostDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(createPostDTO.Authors, "Id", "FullName");

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genre = new SelectList(createPostDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(createPostDTO.Authors, "Id", "FullName");
            return View(await _postService.GetByID(id));

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {

            return View(await _postService.GetPostDetails(id));

        }
    }
}
