using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
