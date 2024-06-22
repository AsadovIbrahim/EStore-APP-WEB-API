using EStore.Application.Repositories.Concretes;
using EStore.Domain.Entities.Concretes;
using EStore.Persistance.Contexts;
using EStore.Persistance.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Repositories.Concretes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategoriesAsync(int page, int size)
        {
            return await _context.Set<Category>()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
                
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Set<Category>().FirstOrDefaultAsync(p => p.Id == id);
            
        }
    }
}
