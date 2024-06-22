using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class UserToken:Entity
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireTime { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
