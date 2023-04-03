using HS_BlogProject.Application.Models.DTOs.AppUserDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);


        Task<UpdateProfileDTO>GetByUserName(string userName);

        Task UpdateUser(UpdateProfileDTO model);

        Task LogOut();
    }
}
