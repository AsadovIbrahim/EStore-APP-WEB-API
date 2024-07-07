using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EStore.Domain.Entities.Concretes;
using EStore.Application.Repositories.Abstracts;

namespace EStore.Application.Repositories.Concretes
{
    public interface IInvoiceRepository:IGenericRepository<Invoice>
    {
        Task AddInvoiceAsync(Invoice invoice);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    }
}
