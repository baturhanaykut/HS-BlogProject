using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Application.Services.GenreService;
using HS_BlogProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace HS_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {

        #region NotWebAPI Dependcy Injection
        //private readonly IGenreService _genreService;

        //public GenreController(IGenreService genreService)
        //{
        //    _genreService = genreService;
        //}
        #endregion
        #region Not WebAPI Index
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    List<GenreVM> genre = await _genreService.GetGenres();

        //    return View(genre);
        //}
        #endregion
        #region NotWebAPICreateGenre
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateGenreDTO genre)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _genreService.Create(genre);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(genre);
        //    }

        //} 
        #endregion
        #region NotWebAPI UpdateGet
        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    return View(await _genreService.GetById(id));
        //}  
        #endregion
        #region NotWebAPI UpdatePost
        [HttpPost]
        //public async Task<IActionResult> Update(UpdateGenreDTO genre)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _genreService.Update(genre);
        //        return RedirectToAction("Index");
        //    }
        //    return View(genre);
        //}  
        #endregion
        #region NotWebAPI First Delete
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //   return View(await _genreService.GetById(id));
        //}
        #endregion
        #region NotWebAPI Delete
        //[HttpPost]
        //[ActionName("Delete")] 
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _genreService.Delete(id);
        //    return RedirectToAction("Index");
        //} 
        #endregion



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<GenreVM> genreList = new List<GenreVM>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7287/api/Genre/GetAllGenre/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    genreList = JsonConvert.DeserializeObject<List<GenreVM>>(apiResponse);
                }
            }
            return View(genreList);
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
                CreateGenreDTO createGenreDTO = new CreateGenreDTO();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(genre), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:7287/api/Genre/AddGenre/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        createGenreDTO = JsonConvert.DeserializeObject<CreateGenreDTO>(apiResponse);
                    }
                }

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
            UpdateGenreDTO updateGenreDTO = new UpdateGenreDTO();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7287/api/Genre/GetGenre/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updateGenreDTO = JsonConvert.DeserializeObject<UpdateGenreDTO>(apiResponse);
                }
            }
            return View(updateGenreDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(genre), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:7287/api/Genre/UpdateGenre", content)) { }
                }
                return RedirectToAction("Index");
            }
            return View(genre);
        }


        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7287/api/Genre/DeleteGenre/" + id)) { }
            }
            return RedirectToAction("Index");

        }

    }
}
