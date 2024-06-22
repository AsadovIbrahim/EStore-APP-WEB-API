using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class Role:Entity
    {
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<User> Users { get; set; } = new List<User>();

        public override string ToString()
        {
            return Name;
        }
    }
}
