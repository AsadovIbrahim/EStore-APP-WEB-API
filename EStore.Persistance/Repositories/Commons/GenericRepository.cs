using EStore.Application.Repositories.Abstracts;
using EStore.Domain.Entities.Abstracts;
using EStore.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Repositories.Commons
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity, new()
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var data=await _context.Set<T>().FirstOrDefaultAsync(p=>p.Id==id);
            _context.Set<T>().Remove(data!);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, bool isDeleted)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == isDeleted);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
