using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.PostDTOs;
using HS_BlogProject.Application.Models.VMs.AuthorVMs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using HS_BlogProject.Application.Models.VMs.PostVMs;
using HS_BlogProject.Entities;
using HS_BlogProject.Enums;
using HS_BlogProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }

        public async Task Create(CreatePostDTO model)
        {

            //Post post = new Post()
            //{
            //    AuthorId = model.AuthorId,
            //    Title = model.Title,
            //    Content = model.Content,
            //    GenreId = model.GenreId,

            //};

            Post post = _mapper.Map<Post>(model);

          
            //Post'un resmi varsa veritabanına yolu yazılmalı. Server üzerindeki bir klasörede resmin kendisi eklenmeli. 

            if (post.UploadPath is not null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                //Resize
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();

                image.Save($"wwwroot/images/{guid}.jpg");  // folder'ın altına kaydettik.

                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = $"/images/defaultpost.jpg";
            }

            await _postRepository.Create(post);
        }

        // Kullanıcının veritabanında Post oluşturabilmesi View sayfasında modeli doldurmasını bekiyoruz. 
        //Controller --> View'a model gönderiyorum. View'da doldurup Controler'a geri döndürsün. 
        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
               
                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM()
                    {
                        Id = x.Id,
                        Name = x.Name
                    
                    },
                    where: x => x.Status != Enums.Status.Passive, // silinmemiş olanlar
                    orderBy: x => x.OrderBy(x => x.Name)
                    ),

                Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        FullName = x.FullName

                       

                    },
                    where: x => x.Status != Enums.Status.Passive, // silinmemiş olanlar
                    orderBy: x=>x.OrderBy(x=>x.FirstName)
                    )
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);
            post.DeleteTime = DateTime.Now;
            post.Status = Enums.Status.Passive;

            await _postRepository.Delete(post);
        }

        public async Task<UpdatePostDTO> GetByID(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)
                );

            model.Genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)
                );


            return model;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var post = await _postRepository.GetFilteredList(
                select: x => new PostVM()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    ImagePath = x.ImagePath
                   

                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre)
                              .Include(x => x.Author)
                );

            return post;
        }

        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,

                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = DateTime.Now,
                },
                where: x => x.Id == id,
                orderBy: null,
                include : x=>x.Include(x=>x.Author)
                );

            return post;

        }


        public async Task<List<GetPostVM>> GetPostsForMembers()
        {
            var posts = await _postRepository.GetFilteredList(
              select: x => new GetPostVM()
              {
                  AuthorFirstName = x.Author.FirstName,
                  AuthorLastName = x.Author.LastName,
                  AuthorImagePath = x.Author.ImagePath,
                  Content = x.Content,
                  CreateDate = x.CreateDate,
                  Id = x.Id,
                  ImagePath = x.ImagePath,
                  Title = x.Title
              },
              where: x => x.Status != Status.Passive,
              orderBy: x => x.OrderByDescending(x => x.CreateDate)
              );

            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
           var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = model.ImagePath;
            }

            await _postRepository.Update(post);
        }
    }
}
