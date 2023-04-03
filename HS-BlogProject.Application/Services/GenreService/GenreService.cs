using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Entities;
using HS_BlogProject.Enums;
using HS_BlogProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper, IPostRepository postRepository)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
          
        }

        public async Task Create(CreateGenreDTO model)
        {
            Genre genre = _mapper.Map<Genre>(model);

            await _genreRepository.Create(genre);

        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            genre.DeleteTime=DateTime.Now;
            genre.Status = Enums.Status.Passive;

            await _genreRepository.Delete(genre);
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x=>x.Id == id);

            var model = _mapper.Map<UpdateGenreDTO>(genre);

            return model;
        }

        public async Task<List<GenreVM>> GetGenre()
        {
            var genre = await _genreRepository.GetFilteredList(
                select: x => new GenreVM()
                {
                    Id =x.Id,
                    Name=x.Name,
                },
                where : x=>x.Status != Status.Passive,
                orderBy: x=>x.OrderBy(x=>x.Name)
                );

            return genre;
         }

        public async Task Update(UpdateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);

            await _genreRepository.Update(genre);
        }
    }
}
