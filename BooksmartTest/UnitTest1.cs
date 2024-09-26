using BooksmartAPI.Models;
using BooksmartAPI.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BooksmartTest
{
    public class UnitTest1
    {
        private readonly Mock<UserManager<User>> _userManager;
        private readonly OrderService _orderService;
        private readonly AuthService _authService;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public UnitTest1()
        {
            _userManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _userService = new UserService(_userManager.Object);
        }

        [Fact]
        public async void GetUser_Exists()
        {
            var email = "test@test.dk";
            var user = new User { Email = email };
            _userManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);

            var result = await _userService.GetUser(email);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetUser_DoesNotExist()
        {
            var email = "test@test.dk";
            _userManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync((User)null);

            var result = await _userService.GetUser(email);

            Assert.Null(result);
        }
    }
}