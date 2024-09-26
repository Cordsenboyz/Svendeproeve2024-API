using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Auth;
using BooksmartAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BooksmartAPI.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            UnitOfWork unitOfWork,
            IClaimsService claimsService,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO login)
        {
            User? user = await _userManager.FindByEmailAsync(login.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                List<Claim> userClaims = await _claimsService.GetUserClaimsAsync(user);

                JwtSecurityToken token = _jwtTokenService.GetJwtToken(userClaims);

                await _userManager.UpdateAsync(user);

                return new LoginResponseDTO
                {
                    Authenticated = true,
                    Token = new TokenDTO
                    {
                        Value = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    }
                };
            }

            return new LoginResponseDTO
            {
                Authenticated = false
            };
        }
        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            User user = registerDTO.Adapt<User>();
            user.Name = $"{registerDTO.FirstName} {registerDTO.LastName}";
            user.UserName = $"{registerDTO.FirstName}{registerDTO.LastName}";

            IdentityResult? result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return result;

            return result;
        }
    }
}
