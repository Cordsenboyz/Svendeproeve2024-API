using BooksmartAPI.Data;
using BooksmartAPI.DTOs.User;
using BooksmartAPI.Models;
using BooksmartAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BooksmartAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserService _userService;

        public UserController(UnitOfWork unitOfWork, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(string email)
        {
            GetUserDTO user = await _userService.GetUser(email);

            return Ok(user);
        }
    }
}
