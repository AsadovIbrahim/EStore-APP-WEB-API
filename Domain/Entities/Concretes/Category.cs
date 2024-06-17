using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class Category:Entity
    {
        public string Name { get; set; }

        public ICollection<Product>Products { get; set; }
    }
}
