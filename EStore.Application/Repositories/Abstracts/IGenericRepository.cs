using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Repositories.Abstracts
{
    public interface IGenericRepository<T>where T : Entity,new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id, bool isDeleted);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
