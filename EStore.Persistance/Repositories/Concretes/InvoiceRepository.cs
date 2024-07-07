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
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Set<Invoice>().AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Set<Invoice>().Where(p=>!p.IsDeleted).ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Set<Invoice>().FirstOrDefaultAsync(i => i.Id == id); 
        }
    }
}
