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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetByCategoryNameAsync(string categoryName)
        {
            return await _context.Set<Product>().Where(p => p.Category.Name == categoryName).ToListAsync();
        }
    }
}
