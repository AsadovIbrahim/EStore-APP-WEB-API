using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.ViewModels.Invoice
{
    public class AddInvoiceVM
    {
        public int Barcode { get; set; }
        public string InvoiceType { get; set; }
        public int CashierId { get; set; }
        public int CustomerId { get; set; }
    }
}
