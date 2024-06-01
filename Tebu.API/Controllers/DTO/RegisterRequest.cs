using Tebu.API.Enums;

namespace Tebu.API.Controllers.DTO
{
    public class RegisterRequest
    {

        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
