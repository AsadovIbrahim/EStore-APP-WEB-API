using EStore.Application.Repositories.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Repositories.Concretes
{
    public interface IUserTokenRepository:IGenericRepository<UserToken>
    {
        Task<UserToken> AddAsync(TokenDTO dto, User user);
        Task<UserToken?> GetByNameAsync(string tokenName);
        Task<UserToken?> GetByToken(string token);
        Task<UserToken?> GetByUserIdAsync(int userId, string tokenName);
    }
}
