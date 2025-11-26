using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Interfaces;

namespace IK.DAL.Repositories.Abstracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        //Queries
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
        /*Bu, veritabanı sorgusunu temsil eden bir nesnedir.Gerçek veriyi hemen getirmez sadece “hangi sorgunun çalıştırılacağını” tanımlar.Örnek olarak tüm çalışanları getir değil,belirli bir departmandaki çalışanları getir gibi koşullu sorgular çalıştırmak için kullanılır.Kod generic hale gelir (her entity için kullanılabilir)
        */

        //Commands
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
