using Tebu.API.Enums;
using Tebu.API.Exceptions;
using Tebu.API.Repository;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Service
{
    public class UserService
    {


        private readonly CurrentUserService currentUserService;
        private readonly UserRepository userRepository;
        public UserService(UserRepository userRepository, CurrentUserService currentUserService)
        {
            this.userRepository = userRepository;
            this.currentUserService = currentUserService;   
        }


        public UserDTO GetUser(string phone, string password)
        {
            var user = userRepository.FindBy(s => s.PhoneNumber == phone && s.Password == password).FirstOrDefault(); //USE HASHING TODO

            if (user == null)
            {
                throw new CustomException("Wrong phone number or password.", 404);
            }

            return user.ConvertToUserDTO();
        }


        public UserDTO Register(string password, string phoneNumber, string email, string name, string surname, UserRole userRole)
        {
            var user = new Data.Models.User
            {
                Name = name,
                Email = email,
                Password = password, // USE HASHING TODO,
                PhoneNumber = phoneNumber,
                Surname = surname,
                UserRole = userRole
            };

            userRepository.Add(user);
            try
            {

                userRepository.SaveChanges();
            }
            catch (Exception)
            {
                throw new CustomException("Check your email and phone number, this may be exist", 409);
            }

            return user.ConvertToUserDTO();
        }
    }
}
