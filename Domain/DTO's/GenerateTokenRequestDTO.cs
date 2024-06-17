using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class GenerateTokenRequestDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
