using BooksmartAPI.Data;
using BooksmartAPI.DTOs.User;
using BooksmartAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace BooksmartAPI.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserDTO> GetUser(string email)
        {
            GetUserDTO getUserDTO = new();

            User? user = await _userManager.FindByEmailAsync(email);
            if (user is null) return null;



            return getUserDTO;
        }
    }
}
