using EStore.Domain.Entities.Abstracts;
using EStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class Invoice:Entity
    {
        public string? InvoiceBarcode { get; set; }
        public int Barcode { get; set; }
        public InvoiceType InvoiceType { get; set; }

        // Foreign Keys
        public int CashierId { get; set; }
        public int CustomerId { get; set; }
        public int? SellInvoiceId { get; set; }

        // Navigation Properties
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
