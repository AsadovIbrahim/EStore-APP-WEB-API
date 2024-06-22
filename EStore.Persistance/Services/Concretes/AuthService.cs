using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using EStore.Persistance.Repositories.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Services.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, ITokenService tokenService, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);

            if (user == null || !VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponseDTO
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Invalid credentials."
                };
            }

            var accessToken = _tokenService.GenerateAccessToken(new GenerateTokenRequestDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) }
            });

            var refreshToken = _tokenService.GenerateRefreshToken("RefreshToken");

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);

            if (principal == null)
            {
                return new LoginResponseDTO
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Invalid refresh token."
                };
            }

            var userName = principal.Identity?.Name;
            var user = await _userRepository.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return new LoginResponseDTO
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "User not found."
                };
            }

            var accessToken = _tokenService.GenerateAccessToken(new GenerateTokenRequestDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) }
            });

            var newRefreshToken = _tokenService.GenerateRefreshToken("RefreshToken");

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                StatusCode = HttpStatusCode.OK
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public async Task RegisterAsync(RegisterRequestDTO registerDTO)
        {
            if (await _userRepository.UserExistsAsync(registerDTO.Email))
                throw new Exception("User already exists.");

            if (registerDTO.Password != registerDTO.ComfirmPassword)
                throw new Exception("Passwords do not match.");

            CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = registerDTO.Username,
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = (await _roleRepository.GetRoleByNameAsync("User")).Id
            };

            await _userRepository.AddAsync(user);
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
