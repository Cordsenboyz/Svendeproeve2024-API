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
        private readonly RoleManager<Role> _roleManager;
        private readonly UnitOfWork _unitOfWork;

        public UserService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            UnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserDTO> GetUser(string email)
        {
            GetUserDTO getUserDTO = new();

            User? user = await _userManager.FindByEmailAsync(email);
            if (user is null) return getUserDTO;



            return getUserDTO;
        }
    }
}
