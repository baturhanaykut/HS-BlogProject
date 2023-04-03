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
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        /* İçindeki Methodlar
         * Create, Update, Delete
         * Any, GetDefault, GetDefaults(Expression)
         * GetfiltredFirstorDefault, GetFilteredList ResultSet Dönücek.
         */


        //Task : Asenkron olarak çalışmasını istiyoruz. 

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);  // veritabanında silme işlemi yapmam, status'ü pasife çekeriz.

        Task<bool> Any(Expression<Func<T, bool>> expression);  // Kayıt varsa true, yoksa false döner.


        Task<T> GetDefault(Expression<Func<T, bool>> expression); // Dinamik olarak where işlemi sağlar. Id ye göre getir.
 
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); // GenreId = 5 olan postları döndür. 


        // Select, Where, Sıralama, Join
        // Hem select, hem order by yapabileceğimiz. Post, Aouthor, Comment, Like'ları birlikte çekmek için include etmek gerekir. Bir sorguya birden fazla tablo girecek. eageloading kullanacağız. 
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select, //Select
            Expression<Func<T, bool>> where,     //Where
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // Sıralama için kullanıyoruz.
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );   

        //Çoklu Dönecek
        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select, //Select
            Expression<Func<T, bool>> where,     //Where
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // Sıralama için kullanıyoruz.
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );   
            
            


        

    }
}
