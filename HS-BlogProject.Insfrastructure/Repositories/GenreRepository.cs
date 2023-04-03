using HS_BlogProject.Entities;
using HS_BlogProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Insfrastructure.Repositories
{
    public class GenreRepository:BaseRepository<Genre>,IGenreRepository
    {
        public GenreRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
