using HS_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS_BlogProject.Application.Models.DTOs.PostDTOs;
using HS_BlogProject.Application.Models.VMs.AuthorVMs;
using HS_BlogProject.Application.Models.VMs.PostVMs;
using HS_BlogProject.Application.Services.AuthorService;
using HS_BlogProject.Application.Services.GenreService;
using HS_BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HS_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            List<AuthorVM> author = await _authorService.GetAuthor();

            return View(author);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Create(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _authorService.GetByID(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDTO author)
        {

            if (ModelState.IsValid)
            {

                await _authorService.Update(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _authorService.GetByID(id));

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            await _authorService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
