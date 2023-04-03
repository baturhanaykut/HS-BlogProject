using HS_BlogProject.Application.Models.DTOs.PostDTOs;
using HS_BlogProject.Application.Models.VMs.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.PostService
{
    public interface IPostService
    {
        // Post Create
        Task Create(CreatePostDTO model);

        //Post Update
        Task Update(UpdatePostDTO model);

        //Post Delete
        Task Delete(int id);

        //Id ile post getir
        Task<UpdatePostDTO> GetByID(int id);

        //Tüm postları getir ve View sayfasında görüntülemek için kullanacağız
        Task<List<PostVM>> GetPosts();

        //Kullanıcıya bir post'un detaylarını gösteriyoruz.
        Task<PostDetailsVM> GetPostDetails(int id);


        Task<CreatePostDTO> CreatePost();


        Task<List<GetPostVM>> GetPostsForMembers();

    }
}
