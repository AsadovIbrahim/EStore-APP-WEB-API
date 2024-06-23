using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                await _authService.RegisterAsync(registerRequestDTO);
                
                return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDTO);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(new { Message = response.ErrorMessage });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromForm] string token)
        {
            try
            {
                var response = await _authService.RefreshTokenAsync(token);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(new { Message = response.ErrorMessage });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmDTO emailConfirmDTO)
        {
            try
            {
                var result = await _authService.ConfirmEmailAsync(emailConfirmDTO);
                if (!result)
                {
                    return BadRequest(new { Message = "Email confirmation failed." });
                }
                return Ok(new { Message = "Email confirmed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] PasswordForgotDTO passwordForgotDTO)
        {
            try
            {
                await _authService.PasswordForgotAsync(passwordForgotDTO);
                return Ok(new { Message = "Password reset email sent. Please check your inbox." });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
