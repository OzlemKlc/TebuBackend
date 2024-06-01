using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tebu.API.Controllers.DTO
{
    public class LoginRequest
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
