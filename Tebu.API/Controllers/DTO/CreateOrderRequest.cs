using Tebu.API.Enums;

namespace Tebu.API.Controllers.DTO
{
    public class CreateOrderRequest
    {
        public OrderType OrderType { get; set; }
        public int VehicleId { get; set; }
        public int AddressId { get; set; }
        public string OrderNote { get; set; }
    }
}
