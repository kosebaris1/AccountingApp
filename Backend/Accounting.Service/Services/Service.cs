using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository; 
        private readonly IUnitOfWorks _unitOfWorks;

        public Service(IUnitOfWorks unitOfWorks, IGenericRepository<T> repository)
        {
            _unitOfWorks = unitOfWorks;
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            await _repository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public void ChangeStatus(T entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _repository.ChangeStatus(entity);
            _unitOfWorks.commit();
        }

        public int Count()
        {
            return _repository.Count();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
