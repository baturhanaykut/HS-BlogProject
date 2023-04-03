using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Models.VMs.GenreVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Application.Services.GenreService
{
    public interface IGenreService
    {
        Task Create(CreateGenreDTO model);

        //Genre Update
        Task Update(UpdateGenreDTO model);

        //Genre Delete
        Task Delete(int id);

        //Id ile genre getir
        Task<UpdateGenreDTO> GetById(int id);

        Task<List<GenreVM>> GetGenre();
     


    }
}
