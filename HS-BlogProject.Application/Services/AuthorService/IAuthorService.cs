using HS_BlogProject.Application.Models.DTOs.AuthorDTOs;
using HS_BlogProject.Application.Models.VMs.AuthorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        // Author Create
        Task Create(CreateAuthorDTO model);

        //Author Update
        Task Update(UpdateAuthorDTO model);

        //Author Delete
        Task Delete(int id);

        //Id ile Author getir
        Task<UpdateAuthorDTO> GetByID(int id);

        //Tüm Author getir ve View sayfasında görüntülemek için kullanacağız
        Task<List<AuthorVM>> GetAuthor();

        //Kullanıcıya bir post'un detaylarını gösteriyoruz.
        Task<AuthorVM> GetAuthorDetails(int id);

    }
}
