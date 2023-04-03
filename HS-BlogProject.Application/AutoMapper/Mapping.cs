using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.AppUserDTOs;
using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.DTOs.PostDTOs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Application.Models.VMs.PostVMs;
using HS_BlogProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();

            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<Post, PostDetailsVM>().ReverseMap();

            CreateMap<PostVM, UpdatePostDTO>().ReverseMap();


            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();

            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();


        }
    }
}
