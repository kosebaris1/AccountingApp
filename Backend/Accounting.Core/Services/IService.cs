using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Services
{
    public interface IService<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        int Count();
        void UpdateAsync(T entity);
        void ChangeStatusAsync(T entity);

        Task AddAsync(T entity);


    }
}
