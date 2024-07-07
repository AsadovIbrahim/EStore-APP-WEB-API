using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface IInvoiceService
    {
        Task AddInvoiceAsync(AddInvoiceVM addInvoiceVM);
        Task<IEnumerable<InvoiceVM>> GetAllInvoicesAsync();
        Task<InvoiceVM> GetInvoiceByIdAsync(int id);
        Task DeleteInvoiceAsync(int id);
        Task CreateRefundInvoiceAsync(int invoiceId);
    }
}
