using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public string CreateAccessToken(AccessTokenDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public UserToken CreateRefreshToken()
        {
            var refreshToken = new UserToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireTime = DateTime.UtcNow.AddMinutes(30),
                CreatedAt = DateTime.UtcNow,
                Name = "refresh"
            };
            return refreshToken;
        }

        public UserToken CreateRepasswordToken()
        {
            var repasswordToken = new UserToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireTime = DateTime.UtcNow.AddMinutes(30),
                CreatedAt = DateTime.UtcNow,
                Name = "repassword"
            };
            return repasswordToken;
        }

        public UserToken CreateConfirmEmailToken()
        {
            var repasswordToken = new UserToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireTime = DateTime.UtcNow.AddMinutes(30),
                CreatedAt = DateTime.UtcNow,
                Name = "confirm"
            };
            return repasswordToken;
        }
    }
}
