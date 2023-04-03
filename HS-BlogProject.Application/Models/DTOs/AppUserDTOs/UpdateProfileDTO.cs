using HS_BlogProject.Application.Extension;
using HS_BlogProject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.AppUserDTOs
{
    public class UpdateProfileDTO
    {
        //Todo DataAnatonsins yazacağız.

        public string Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Email { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status { get; set; } = Status.Modified;

        [ValidateNever]
        public string ImagePath { get; set; }

        //ToDo
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
    }
}
