using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface ITokenService
    {
        string CreateAccessToken(AccessTokenDTO user);
        UserToken CreateRefreshToken();
        UserToken CreateRepasswordToken();
        UserToken CreateConfirmEmailToken();
    }
}
