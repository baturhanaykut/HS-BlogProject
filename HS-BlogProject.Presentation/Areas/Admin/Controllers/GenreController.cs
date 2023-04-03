using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace HS_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<GenreVM> genre = await _genreService.GetGenre();

            return View(genre);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Create(genre);
                return RedirectToAction("Index");
            }
            else
            {
                return View(genre);
            }
          
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _genreService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Update(genre);
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View(await _genreService.GetById(id));

        //}

        //[HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
