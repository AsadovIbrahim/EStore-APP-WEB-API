using Azure.Core;
using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.Entities.Concretes;
using EStore.Domain.Enums;
using EStore.Domain.ViewModels.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Services.Concretes
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public async Task AddInvoiceAsync(AddInvoiceVM addInvoiceVM)
        {
            Random random = new Random();
            string barcodeString = "";
            for (int i = 0; i < 12; i++)
            {
                barcodeString += random.Next(0, 10).ToString();
            }
            long barcode = long.Parse(barcodeString);
            var invoice = new Invoice
            {
                Barcode = barcode,
                CashierId = addInvoiceVM.CashierId,
                CustomerId = addInvoiceVM.CustomerId,
                InvoiceItems = addInvoiceVM.InvoiceItems,
                InvoiceType = InvoiceType.Sell
            };
            await _invoiceRepository.AddInvoiceAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task CreateRefundInvoiceAsync(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            invoice.InvoiceType = InvoiceType.Refund;
            await _invoiceRepository.AddInvoiceAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _invoiceRepository.Delete(id);
            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvoiceVM>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var result = invoices.Select(p => new InvoiceVM
            {
                Id = p.Id,
                Barcode = p.Barcode,
                CashierId = p.CashierId,
                CustomerId = p.CustomerId,
                InvoiceType = p.InvoiceType,
            }).ToList();

            return result;
        }

        public async Task<InvoiceVM> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            var result = new InvoiceVM
            {
                Id = invoice.Id,
                Barcode = invoice.Barcode,
                CashierId = invoice.CashierId,
                CustomerId = invoice.CustomerId,
                InvoiceType = invoice.InvoiceType,
            };
            return result;
        }
    }
}
