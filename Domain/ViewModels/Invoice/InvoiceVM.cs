using EStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.ViewModels.Invoice
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        public long Barcode { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public int CashierId { get; set; }
        public int CustomerId { get; set; }
    }
}
