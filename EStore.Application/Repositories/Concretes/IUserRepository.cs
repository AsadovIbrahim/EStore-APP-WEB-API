using EStore.Application.Repositories.Abstracts;
using EStore.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Repositories.Concretes
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task RegisterAsync(User user);
        Task<User?> GetByEmailAsync(string? email);
        Task<User?> GetByEmailWithRolesAsync(string? email);
        Task<User?> GetByUsernameAsync(string? username);
        Task<User?> GetByUsernameWithRolesAsync(string? username);
        Task<User?> GetUserByIdAsync(int id);
    }
}
