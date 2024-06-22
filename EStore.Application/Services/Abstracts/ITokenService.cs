using EStore.Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(GenerateTokenRequestDTO generateTokenRequestDTO);
        TokenDTO GenerateRefreshToken(string tokenName);
    }
}
