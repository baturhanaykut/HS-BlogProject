using AutoMapper;
using HS_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS_BlogProject.Application.Models.VMs.CommentVMs;
using HS_BlogProject.Entities;
using HS_BlogProject.Enums;
using HS_BlogProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateCommentDTO model)
        {
            var comment = _mapper.Map<Comment>(model);
            await _commentRepository.Create(comment);

        }

        public async Task Delete(int id)
        {
            Comment comment = await _commentRepository.GetDefault(x => x.Id == id);
            comment.Status = Status.Passive;
            await _commentRepository.Delete(comment);
        }

        public async Task<List<CommentVM>> GetComment()
        {
            var comment = await _commentRepository.GetFilteredList(
                select: x => new CommentVM()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                },
                where: x=>x.Status != Status.Passive,
                orderBy: x=>x.OrderBy(x=>x.Title)
                  );
            return comment;
        }

        public async Task Update(UpdateCommentDTO model)
        {
            var comment = _mapper.Map<Comment>(model);
            await _commentRepository.Update(comment);
        }
    }
}
