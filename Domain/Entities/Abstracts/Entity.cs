using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Abstracts
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
