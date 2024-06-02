using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tebu.API.Controllers.DTO;
using Tebu.API.Data.Models;
using Tebu.API.Service;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private CurrentUserService currentUserService;
        private UserService userService;

        public LoginController(UserService userService, CurrentUserService currentUserService)
        {
            this.userService = userService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("login")]
        public async Task<UserResponse> Login([FromBody] LoginRequest request)
        {
            UserDTO user = userService.GetUser(request.Phone, request.Password);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, Enum.GetName(user.UserRole)),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return new UserResponse
            {
                User = user
            };
        }

        [HttpGet("current-user")]
        public async Task<UserResponse> GetCurrentUser()
        {
            UserDTO user = currentUserService.User;

            return new UserResponse
            {
                User = user
            };
        }

        [HttpPost("register")]
        public async Task<UserResponse> Register([FromBody] RegisterRequest request)
        {
            UserDTO user = userService.Register(request.Password, request.PhoneNumber, request.Email, request.Name, request.Surname, Enums.UserRole.Customer);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, Enum.GetName(user.UserRole)),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return new UserResponse
            {
                User = user
            };
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


    }
}
