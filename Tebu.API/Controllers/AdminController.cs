using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Controllers.DTO;
using Tebu.API.Repository;
using Tebu.API.Service;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private CurrentUserService currentUserService;
        private UserService userService;
        private AdminService adminService;
        private UserRepository userRepository;

        public AdminController(CurrentUserService currentUserService, UserService userService, AdminService adminService, UserRepository userRepository)
        {
            this.currentUserService = currentUserService;
            this.userService = userService;
            this.adminService = adminService;
            this.userRepository = userRepository;
        }

        [HttpPost("create-worker")]
        [Authorize(Roles = "Admin")]
        public UserResponse CreateWorker([FromBody]RegisterRequest request)
        {
            var user = userService.Register(request.Password, request.PhoneNumber, request.Email, request.Name, request.Surname, Enums.UserRole.Worker);

            return new UserResponse
            {
                User = user,
            };
        }


        [HttpGet("get-statistics")]
        [Authorize(Roles = "Admin")]
        public StatisticsResponse GetStatistics()
        {
            return this.adminService.GetStatistics();
        }

        [HttpGet("get-workers")]
        [Authorize(Roles = "Admin")]
        public List<UserDTO> GetWorkers([FromQuery] int count, [FromQuery] int pageIndex)
        {
            return userRepository.GetAll().Where(s => s.UserRole == Enums.UserRole.Worker).Skip(pageIndex * count).OrderByDescending(s => s.Id).Take(count).ToList().Select(s => s.ConvertToUserDTO()).ToList();
        }

    }
}
