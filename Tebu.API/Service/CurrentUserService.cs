using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Data.Models;
using Tebu.API.Repository;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Service
{
    public class CurrentUserService
    {
        public UserDTO User { get; private set; }
        internal User UserEntity { get; private set; } // should stay internal, we only can reach this in services.

        private readonly UserRepository userRepository;
        public CurrentUserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool TryToSetCurrentUser(string userId)
        {
            if (userId == null)
            {
                this.User = null;
                return false;
            }

            var user = userRepository.FindBy(s => s.Id == Convert.ToInt32(userId)).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            this.UserEntity = user;
            this.User = user.ConvertToUserDTO();

            return true;
        }

        public void SetCurrentUser(UserDTO user)
        {
            this.User = user;
            this.UserEntity = userRepository.Get(user.Id); //this entity already is loaded to context, so we are not doing additional call to db, its harmless.
        }

    }
}
