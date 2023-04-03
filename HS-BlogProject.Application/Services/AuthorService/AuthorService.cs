using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS_BlogProject.Application.Models.VMs.AuthorVMs;
using HS_BlogProject.Entities;
using HS_BlogProject.Enums;
using HS_BlogProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            Author author = _mapper.Map<Author>(model);
            await _authorRepository.Create(author);
        }

       
        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);
            author.DeleteTime = DateTime.Now;
            author.Status = Enums.Status.Passive;

            await _authorRepository.Delete(author);
        }

        public async Task<List<AuthorVM>> GetAuthor()
        {
            var author = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                },
                where:x=>x.Status  != Status.Passive,
                orderBy: x=>x.OrderBy(x=>x.FirstName)
                ); 
            return author;
        }

        public async Task<AuthorVM> GetAuthorDetails(int id)
        {
            var author = await _authorRepository.GetFilteredFirstOrDefault(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)
                );

            return author;
        }

        public async Task<UpdateAuthorDTO> GetByID(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateAuthorDTO>(author);

            return model;
        }


        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);
            await _authorRepository.Update(author);
        }
    }
}
