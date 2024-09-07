using BooksmartAPI.DTOs.Auth;
using BooksmartAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksmartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            LoginResponseDTO responseDTO = await _authService.Login(loginDTO);
            if (!responseDTO.Authenticated) return BadRequest("Email or Password is wrong.");

            return Ok(responseDTO);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword) return BadRequest("Passwords do not match.");

            IdentityResult result = await _authService.Register(registerDTO);
            if (!result.Succeeded) return BadRequest(result.Errors.Select(e => e.Description));

            return Created();
        }
    }
}
