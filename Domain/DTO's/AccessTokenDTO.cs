using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class AccessTokenDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
