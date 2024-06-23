using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using EStore.Infrastructure.Services;
using EStore.Persistance.Repositories.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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

            if (user == null ||user.UserName!=loginDTO.UserName ||user.Email!=loginDTO.Email 
                || !VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
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
            if (registerDTO == null) throw new ArgumentNullException(nameof(registerDTO));

            if (await _userRepository.UserExistsAsync(registerDTO.Email))
                throw new Exception("User already exists.");

            if (registerDTO.Password != registerDTO.ComfirmPassword)
                throw new Exception("Passwords do not match.");

            CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var role = await _roleRepository.GetRoleByNameAsync("Cashier");
            if (role == null) throw new Exception("User role not found.");

            var user = new User
            {
                UserName = registerDTO.Username,
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = role.Id
            };
            var emailtoken=GenerateEmailConfirmationToken(user);
            await SmtpService.SendMail(user.Email, "Confirm Your Email", $"<a href='{emailtoken}'>Confirm Your Email</a>"); 
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

        public async Task PasswordForgotAsync(PasswordForgotDTO passwordForgotDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(passwordForgotDTO.Email);
            if(user == null)
            {
                throw new Exception("User not found!");
            }
            var resetToken = GeneratePasswordResetToken();
           

            var resetLink = $"{_configuration["AppSettings:ClientUrl"]}/reset-password?token={resetToken}";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["MailSettings:Mail"], _configuration["MailSettings:DisplayName"]),
                Subject = "Password Reset",
                Body = $"Please reset your password by clicking on the following link: {resetLink}",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(user.Email);

            using (var smtpClient = new SmtpClient("smtp.example.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-email-password");
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }

        }

        public async Task<bool> ConfirmEmailAsync(EmailConfirmDTO emailConfirmDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(emailConfirmDTO.Email);
            if (user == null)
            {
                return false;
            }
            user.EmailConfirmed = true;
            await _userRepository.Update(user);
            return true;
        }
        

        private string GenerateEmailConfirmationToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GeneratePasswordResetToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[32];
                rng.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }


    }
}
