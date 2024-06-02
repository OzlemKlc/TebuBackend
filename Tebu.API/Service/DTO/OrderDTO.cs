using Tebu.API.Enums;

namespace Tebu.API.Service.DTO
{
    public class OrderDTO : BaseDTO
    {
        public OrderStatus Status { get; set; }
        public OrderType OrderType { get; set; }
        public string OrderNote { get; set; }
        public int CustomerId { get; set; }
        public int? WorkerId { get; set; }
        public int VehicleId { get; set; }
        public int AddressId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public UserDTO Customer { get; set; }
        public UserDTO? Worker { get; set; }
        public AddressDTO Address { get; set; }
        public VehicleDTO Vehicle { get; set; }
    }
}
