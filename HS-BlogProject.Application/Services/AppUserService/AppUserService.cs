using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS_BlogProject.Entities;
using HS_BlogProject.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.AppUserService
{


    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        //Dependency Injection
        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        // UserName ile AppUser tablosunda bulunan (eğer varsa) AppUser satırını çekeriz ve UpdateProfileDTO nesnesini doldururuz. 
        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO

                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                },
                where: x => x.UserName == userName
                );
            return result;
        }

        // Kullanıcının sisteme Login olmasını sağlar. User bilgilerine ulaşabiliriz. 
        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }


        //Sistemden çıkış için kullanırız. User bilgileri sessiondan silinir.
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        //yeni bir AppUser oluştururuz.
        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            //gelen RegistorDTO, create edilmesi gereken AppUser

            //AutoMapper kullanacağız. 
            AppUser user = new AppUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            //user.Password = model.ComfirmPassword;
            user.CreateDate = model.CreateDate;

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        // Kullanıcı bilgilerini güncellemek için kullanırız.
        // Kullanıcının güncellemek istediği bilgilieri View'dan UpdateProfileDTO nesnesi aracılığıyla bilgileri alırız. Resim, Email, Password güncellemelerini yaparız. 
        public async Task UpdateUser(UpdateProfileDTO model)
        {
            // Update işlemlerinde önce id ile ilgilli nesneyi Ram'e çekeriz.  Dışarıdan gelen güncel bilgilerle değişiklikleri yaparız. En Son SaveChanges ile veritabanına güncellemeleri gönderiririz. 
            
            //Todo: Mapper

            //var user2 = _mapper.Map<AppUser>(model);

            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            //todo : Uploadd Parh Resim işlemleri

            if (model.UploadPath is not null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                //Resize
                image.Mutate(x => x.Resize(300, 300));

                Guid guid = Guid.NewGuid();

                image.Save($"wwwroot/images/{guid}.jpg");  // folder'ın altına kaydettik.

                user.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                if (model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                else
                {
                    user.ImagePath = $"/images/defaultpost.jpg";

                }
            }
            user.UpdateDate = DateTime.Now;
            user.Status = Enums.Status.Modified;
            user.UserName = model.UserName;
            await _appUserRepository.Update(user);

            //Password değişikliklerinde 
            if (model.Password is not null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                await _userManager.UpdateAsync(user);
            }

            //Email adresi yoksa Email adresi ekletiyoruz.
            if (model.Email != null)
            {
                AppUser isuserEmailExists = await _userManager.FindByEmailAsync(model.Email);
                if (isuserEmailExists == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }

            }

        }
    }
}
