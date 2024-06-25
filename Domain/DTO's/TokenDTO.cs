using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class TokenDTO
    {
        public string? Token { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
