using HS_BlogProject.Entities;
using HS_BlogProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Insfrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>,ICommentRepository
    {
        public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
