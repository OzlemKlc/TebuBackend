using Tebu.API.Enums;

namespace Tebu.API.Controllers.DTO
{
    public class ChangeOrderStatusRequest
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
