using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class InvoiceItem:Entity
    {
        public decimal Quantity { get; set; }

        public int ProductId { get; set; }
        public int InvoiceId { get; set; }


        public Product Product { get; set; }
        public Invoice Invoice { get; set; }
    }
}
