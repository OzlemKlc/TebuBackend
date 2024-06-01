using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Enums;

namespace Tebu.API.Service.DTO
{
    public class UserDTO : BaseDTO
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole UserRole { get; set; }
    }
}
