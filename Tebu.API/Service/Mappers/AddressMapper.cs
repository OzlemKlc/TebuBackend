using Tebu.API.Data.Models;
using Tebu.API.Service.DTO;

namespace Tebu.API.Service.Mappers
{
    public static class AddressMapper
    {
        public static AddressDTO ConvertToAddresDTO(this Address address)
        {
            return new AddressDTO
            {
                City = address.City,
                District = address.District,
                FullAdress = address.FullAdress,
                Id = address.Id,
                Name = address.Name,
                UserId = address.UserId,
            };
        }

    }
}
