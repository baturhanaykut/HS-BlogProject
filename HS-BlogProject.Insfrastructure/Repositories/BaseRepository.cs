using HS_BlogProject.Entities;
using HS_BlogProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace HS_BlogProject.Insfrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        //Context bağlantısına ihtiyacımız var.
        private readonly AppDbContext _appDbContext;
        protected DbSet<TEntity> table;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            table = _appDbContext.Set<TEntity>();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(TEntity entity)
        {
            table.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            await _appDbContext.SaveChangesAsync();  // servis katmanında enity'sine göre pasif hale getireceğiz. 
        }

        public async Task<TEntity> GetDefault(Expression<Func<TEntity, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetDefaults(Expression<Func<TEntity, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> select, 
            Expression<Func<TEntity, bool>> where, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = table; // Select * from Post

            if (where is not null)
            {
                query = query.Where(where);  // Select * from Post where GenreId=3
            }
            if (include is not null)
            {
                query = include(query);         
            }
            if (orderBy is not null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
                return await query.Select(select).FirstOrDefaultAsync();

        }

        public async Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<TEntity, TResult>> select, 
            Expression<Func<TEntity, bool>> where, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = table;
            if (where is not null)
            {
                query = query.Where(where);  
            }
            if (include is not null)
            {
                query = include(query);
            }
            if (orderBy is not null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            _appDbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
