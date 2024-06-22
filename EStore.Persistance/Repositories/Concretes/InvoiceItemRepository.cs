using EStore.Application.Repositories.Concretes;
using EStore.Domain.Entities.Concretes;
using EStore.Persistance.Contexts;
using EStore.Persistance.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Repositories.Concretes
{
    public class InvoiceItemRepository : GenericRepository<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
