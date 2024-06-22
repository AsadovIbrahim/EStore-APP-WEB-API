using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(GenerateTokenRequestDTO generateTokenRequestDTO)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var tokenCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: generateTokenRequestDTO.Claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: tokenCredential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public TokenDTO GenerateRefreshToken(string tokenName)
        {
            var newToken = new TokenDTO();
            newToken.Name = tokenName;
            newToken.CreatedAt = DateTime.UtcNow;
            newToken.ExpireTime = DateTime.UtcNow.AddMonths(1);
            newToken.Token = Guid.NewGuid().ToString().ToLower();
            return newToken;
        }
    }
}
