using HS_BlogProject.Application.Extension;
using HS_BlogProject.Application.Models.VMs.AuthorVMs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Models.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type Title")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Title { get; set; }



        [Required(ErrorMessage = "Must to type Content")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Content { get; set; }


        [ValidateNever]
        public string ImagePath { get; set; }



        // Custom Extension yazacağız.
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }


        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;



        [Required(ErrorMessage = "Must to type Author")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Must to type Genre")]
        public int GenreId { get; set; }



        // Genre ve Author CM listerleri doldurulacak
        [ValidateNever]
        public List<GenreVM>? Genres { get; set; }
        [ValidateNever]
        public List<AuthorVM>? Authors { get; set; }
    }
}
