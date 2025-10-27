using Application;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authHandler;
        private readonly IUserHandler _userHandler;

        public AuthController(IAuthService authHandler, IUserHandler userHandler )
        {
            _authHandler = authHandler;
            _userHandler = userHandler;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
             var securityUser = await _userHandler.GetSecurityUserByEmailAsync(loginDto.Email);
             if (securityUser == null || !_authHandler.VerifyPasswordHash(loginDto.Password, securityUser.PasswordHash, securityUser.PasswordSalt))
             {
                 return Unauthorized("Email ou mot de passe incorrect.");
             }
             var user = await _userHandler.GetUserByEmailAsync(loginDto.Email);
             var token = _authHandler.CreateToken(user);
             return Ok(new { token = token });
        }
    }
}
