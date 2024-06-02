using Tebu.API.Data.Models;
using Tebu.API.Service.DTO;

namespace Tebu.API.Service.Mappers
{
    public static class VehicleMapper
    {
        public static VehicleDTO ConvertToVehicleDTO(this Vehicle vehicle)
        {
            return new VehicleDTO
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                UserId = vehicle.UserId,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
            };
        }
    }
}
