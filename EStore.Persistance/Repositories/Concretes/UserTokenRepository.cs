using EStore.Application.Repositories.Concretes;
using EStore.Domain.DTO_s;
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
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserToken> AddAsync(TokenDTO dto, User user)
        {
            var userToken = new UserToken()
            {
                UserId=user.Id,
                Token=dto.Token,
                Name=dto.Name,
                ExpireTime=dto.ExpireTime,
            };
            await _context.Set<UserToken>().AddAsync(userToken);
            await _context.SaveChangesAsync();
            return userToken;
        }

        public Task<UserToken?> GetByNameAsync(string tokenName)
        {
            return _context.Set<UserToken>().FirstOrDefaultAsync(n=>n.Name==tokenName);
        }

        public Task<UserToken?> GetByToken(string token)
        {
            return _context.Set<UserToken>().FirstOrDefaultAsync(t=>t.Token==token);
        }

        public async Task<UserToken?> GetByUserIdAsync(int userId, string tokenName)
        {
            return await _context.Set<UserToken>().FirstOrDefaultAsync(ut => ut.UserId == userId && ut.Name == tokenName);
        }
    }
}
