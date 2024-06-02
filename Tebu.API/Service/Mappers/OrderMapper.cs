using Tebu.API.Data.Models;
using Tebu.API.Service.DTO;

namespace Tebu.API.Service.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ConvertToOrderDTO(this Order order)
        {
            return new OrderDTO
            {
                AddressId = order.AddressId,
                CreationDate = order.CreationDate,
                CustomerId = order.CustomerId,
                DeliveredDate = order.DeliveredDate,
                Id = order.Id,
                OrderNote = order.OrderNote,
                OrderType = order.OrderType,
                Status = order.Status,
                VehicleId = order.VehicleId,
                WorkerId = order.WorkerId,
                Address = order.Address.ConvertToAddresDTO(),
                Customer = order.Costumer.ConvertToUserDTO(),
                Vehicle = order.Vehicle.ConvertToVehicleDTO(),
                Worker = order.Worker?.ConvertToUserDTO(),
            };
        }
    }
}
