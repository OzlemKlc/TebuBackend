using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Service.DTO;

namespace Tebu.API.Controllers.DTO
{
    public class CurrentUserResponse
    {
        public UserDTO User { get; set; }
    }
}
