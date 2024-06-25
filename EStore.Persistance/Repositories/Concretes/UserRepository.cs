using EStore.Application.Repositories.Concretes;
using EStore.Domain.Entities.Concretes;
using EStore.Persistance.Contexts;
using EStore.Persistance.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Repositories.Concretes
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string? email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<User?> GetByEmailWithRolesAsync(string? email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByTokenAsync(string? token)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(p => p.UserTokens.SingleOrDefault(u => u.Token == token).Token == token);
        }

        public async Task<User?> GetByUsernameAsync(string? username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(p => p.UserName == username);
        }
    }
}
