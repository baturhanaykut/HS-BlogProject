using HS_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HS_BlogProject.Presentation.Controllers
{
    [Authorize] // Bu controller'daki Action'lara yetkisiz kişilerin istekte bulunmasını engellemiş olduruz.
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        
        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        //Kullanıcı ile giriş sayfası 
        [AllowAnonymous] // Actiona erişme izni veriyor. (Authorize ezip)
        public IActionResult Register()
        {

            if (User.Identity.IsAuthenticated) // Eğer hali hazırda sistemde Authenticate olmuş bir kullanıcı varsa Register safyasını göremesin.
            {

                return RedirectToAction("Index", "");
            }

            // Eğer kullanıcı giriş yapmamış ise.
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(registerDTO);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description); // Kayıt için bir model gönderdiğimiz hatalar var ise göstermek için kullanırız. 
                    TempData["Error"] = "Something wnet wron";
                }

            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/") // Logine nereden geldiğimizi tutar
        {

            if (User.Identity.IsAuthenticated) // Eğer hali hazırda sistemde Authenticate olmuş bir kullanıcıvarsa Login sayfasını görmesin.
            {

                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
            ViewData["ReturnUrl"] = returnUrl;

            // Eğer kullanıcı giriş yapmamış ise.
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);
                if (result.Succeeded)
                {
                    return RedirectToLocal(ReturnUrl);
                }

                ModelState.AddModelError("", "Invalid Login Attempt");

            }

            return View(model);

        }

        private IActionResult RedirectToLocal(string returnUrl ="/")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();
            return RedirectToAction("Index", "Home"); // Aynı dizindeki Home Controller'daki Index Actiona'a git
        }


        public async Task<IActionResult> Edit(string username)
        {
            if (username != null)
            {
                UpdateProfileDTO user = await _appUserService.GetByUserName(username);
                return View(user);
            }
            else if (username =="")
            {
                username = HttpContext.User.Identity.Name;
                UpdateProfileDTO user = await _appUserService.GetByUserName(username);
                return View(user);
            }
            else
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _appUserService.UpdateUser(model);
                   
                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went rong";
               
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated";
                return View();
            }
            
        }
        // To Do View Profile Page (ProfileDetails Action ve View)

    }
}
