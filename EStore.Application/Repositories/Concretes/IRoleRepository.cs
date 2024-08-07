﻿using EStore.Application.Repositories.Abstracts;
using EStore.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Repositories.Concretes
{
    public interface IRoleRepository:IGenericRepository<Role>
    {
        Task<Role?> GetRoleByRoleName(string roleName);
    }
}
