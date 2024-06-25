using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using EStore.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserTokenRepository _usertokenRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly ITokenService _tokenService;

        public AuthController(IUserRepository userRepo, ITokenService tokenService, IUserTokenRepository usertokenRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _usertokenRepo = usertokenRepo;
            _roleRepo = roleRepo;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepo.GetByUsernameAsync(loginDTO.UserName);
            user.Role = await _roleRepo.GetByIdAsync(user.RoleId);
            if (user is null)
                return BadRequest("Invalid username");

            if (user.IsEmailConfirmed is false)
                return BadRequest("Email not confirmed");


            using var hmac = new HMACSHA256(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            var isPasswordMatch = computedHash.SequenceEqual(user.PasswordHash);
            if (!isPasswordMatch)
                return BadRequest("Invalid password");

            var accesstokenDTO = new AccessTokenDTO() { Email = user.Email, Username = user.UserName, RoleName = user.Role.Name };
            var accessToken = _tokenService.CreateAccessToken(accesstokenDTO);

            var refreshToken = _tokenService.CreateRefreshToken();
            TokenDTO refreshtokenDTO = new() { CreatedAt = refreshToken.CreatedAt, ExpireTime = refreshToken.ExpireTime, Name = refreshToken.Name, Token = refreshToken.Token };
            SetRefreshToken(user, refreshtokenDTO);

            return Ok(new { accessToken = accessToken });
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh"];
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Invalid refresh token");

            var userToken = await _usertokenRepo.GetByToken(refreshToken);
            var user = userToken.User;
            if (user is null)
                return BadRequest("Invalid refresh token");

            var accesstokenDTO = new AccessTokenDTO() { Email = user.Email, Username = user.UserName, RoleName = user.Role.Name };
            var accessToken = _tokenService.CreateAccessToken(accesstokenDTO);

            var refreshTokenObj = _tokenService.CreateRefreshToken();
            TokenDTO refreshtokenDTO = new() { CreatedAt = refreshTokenObj.CreatedAt, ExpireTime = refreshTokenObj.ExpireTime, Name = refreshTokenObj.Name, Token = refreshTokenObj.Token };


            SetRefreshToken(user, refreshtokenDTO);

            return Ok(new { accessToken = accessToken });
        }



        private void SetRefreshToken(User user, TokenDTO Token)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = Token.ExpireTime
            };

            Response.Cookies.Append(Token.Name, Token.Token, cookieOptions);

            _usertokenRepo.AddAsync(Token, user);

            _userRepo.Update(user);
            _userRepo.SaveChangesAsync();
        }



        [HttpPost("[action]")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var user = await _userRepo.GetByUsernameAsync(registerDTO.Username);
            if (user is not null)
                return BadRequest("User already exists");

            using var hmac = new HMACSHA256();

            var userRole = await _roleRepo.GetRoleByRoleName("Customer");

            var newUser = new User()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                Role = userRole!,
                RoleId = userRole!.Id,
                IsEmailConfirmed = false,
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
            };


            var confirmEmailToken = _tokenService.CreateConfirmEmailToken();
            var actionUrl = $@"https://localhost:5046/api/Auth/ConfirmEmail?token={confirmEmailToken.Token}";
            var result = await SmtpService.SendMail(registerDTO.Email, "Confirm Your Email", $"<a href='{actionUrl}'>Confirm Your Email</a>");



            await _userRepo.AddAsync(newUser);
            await _userRepo.SaveChangesAsync();
            TokenDTO confirmEmailDTO = new() { CreatedAt = confirmEmailToken.CreatedAt, ExpireTime = confirmEmailToken.ExpireTime, Name = confirmEmailToken.Name, Token = confirmEmailToken.Token };
            await _usertokenRepo.AddAsync(confirmEmailDTO, await _userRepo.GetByEmailAsync(newUser.Email));
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {


            var user = await _userRepo.GetByTokenAsync(token);
            if (user is null)
                return BadRequest("Invalid User");

            var userToken = await _usertokenRepo.GetByToken(token);
            if (userToken.ExpireTime < DateTime.UtcNow)
                return BadRequest("Expired Token");


            user.IsEmailConfirmed = true;

            await _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
            return Ok();
        }




        // Forgot Passowrd

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userRepo.GetByEmailAsync(forgotPasswordDTO.Email);
            if (user is null)
                return BadRequest("User not found");

            var repasswordToken = _tokenService.CreateRepasswordToken();
            var actionUrl = $@"https://localhost:5001/api/Auth/ResetPassword?token={repasswordToken.Token}";

            var result = await SmtpService.SendMail(forgotPasswordDTO.Email, "Reset Password", $"Reset your password <a href='{actionUrl}'>clicking here</a>");

            TokenDTO repasswordTokenDTO = new() { CreatedAt = repasswordToken.CreatedAt, ExpireTime = repasswordToken.ExpireTime, Name = repasswordToken.Name, Token = repasswordToken.Token };
            await _usertokenRepo.AddAsync(repasswordTokenDTO, user);


            await _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();

            return Ok(new { actionUrl = actionUrl });
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordDTO resetPasswordDTO)
        {

            var user = await _userRepo.GetByTokenAsync(token);
            if (user is null)
                return BadRequest("Invalid RePasswordToken");

            var userToken = await _usertokenRepo.GetByToken(token);
            if (userToken.ExpireTime < DateTime.UtcNow)
                return BadRequest("RePasswordToken expired");

            using var hmac = new HMACSHA256();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(resetPasswordDTO.Password));
            user.PasswordSalt = hmac.Key;

            await _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
            return Ok();
        }
    }
}
