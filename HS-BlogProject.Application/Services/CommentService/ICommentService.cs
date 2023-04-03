using HS_BlogProject.Application.Models.DTOs.CommentDTOs;
using HS_BlogProject.Application.Models.VMs.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.CommentService
{
    public interface ICommentService 
    {
        Task Create(CreateCommentDTO model);

        Task Update(UpdateCommentDTO model);

        Task Delete(int id);

        Task<List<CommentVM>> GetComment();
    }
}
