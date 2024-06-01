using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Data.Models;
using Tebu.API.Service.DTO;

namespace Tebu.API.Service.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ConvertToUserDTO(this User user)
        {
            return new UserDTO
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Surname = user.Surname,
                UserRole = user.UserRole,
                Id = user.Id,
            };
        }

    }
}
