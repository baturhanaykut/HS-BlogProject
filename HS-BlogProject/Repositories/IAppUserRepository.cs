using HS_BlogProject.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS_BlogProject.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {

    }
}
