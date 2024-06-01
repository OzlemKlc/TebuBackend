using Tebu.API.Enums;

namespace Tebu.API.Data.Models
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }
        public OrderType OrderType { get; set; }
        public string OrderNote { get; set; }
        public int CustomerId { get; set; }
        public int? WorkerId { get; set; }
        public int VehicleId { get; set; }
        public int AddressId { get; set; }

        public virtual User Costumer { get; set; }
        public virtual User Worker { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Address Address { get; set; }
    }
}
