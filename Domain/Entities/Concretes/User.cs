using EStore.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Concretes
{
    public class User:Entity
    {
        public string ?UserName { get; set; }
        public string ?Name { get; set; }
        public string ?Surname { get; set; }
        public string Email { get; set; }
        public bool? IsEmailConfirmed { get; set; }

        public byte[] ?PasswordHash { get; set; }
        public byte[] ?PasswordSalt { get; set; }

        public int RoleId { get; set; } 
        public virtual Role Role { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
