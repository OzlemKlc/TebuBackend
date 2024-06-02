using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Tebu.API.Controllers.DTO;
using Tebu.API.Data.Models;
using Tebu.API.Exceptions;
using Tebu.API.Repository;
using Tebu.API.Service;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderRepository orderRepository;
        private CurrentUserService currentUserService;

        public OrderController(OrderRepository orderRepository, CurrentUserService currentUserService)
        {
            this.orderRepository = orderRepository;
            this.currentUserService = currentUserService;
        }

        //todo take these to services. we have some time management issues here. it will be more time consuming in the long run.

        [HttpPost("create-order")]
        [Authorize(Roles = "Customer")]
        public OrderDTO CreateOrder([FromBody] CreateOrderRequest request)
        {
            var order = new Order
            {
                AddressId = request.AddressId,
                CustomerId = currentUserService.User.Id,
                CreationDate = DateTime.UtcNow,
                DeliveredDate = null,
                OrderNote = request.OrderNote,
                OrderType = request.OrderType,
                Status = Enums.OrderStatus.WaitingToGetAccepted,
                VehicleId = request.VehicleId,
                WorkerId = null
            };

            orderRepository.Add(order);
            orderRepository.SaveChanges();

            order = orderRepository.FindBy(s => s.Id == order.Id).Include(s => s.Vehicle).Include(s => s.Address).First();

            return order.ConvertToOrderDTO();
        }

        [HttpGet("get-customer-orders")]
        [Authorize(Roles = "Customer")]
        public List<OrderDTO> GetCustomerOrders([FromQuery] int count, [FromQuery] int pageIndex)
        {
            return orderRepository.FindBy(s => s.CustomerId == currentUserService.User.Id).OrderByDescending(s => s.Id).Skip(pageIndex * count).Take(count).ToList().Select(s => s.ConvertToOrderDTO()).ToList();
        }


        [HttpGet("get-worker-orders")]
        [Authorize(Roles = "Worker")]
        public List<OrderDTO> GetWorkerOrders([FromQuery] int count, [FromQuery] int pageIndex)
        {
            return orderRepository
                .FindBy(s => s.WorkerId == null || s.WorkerId == currentUserService.User.Id)
                .OrderByDescending(s => (s.WorkerId == currentUserService.User.Id && s.DeliveredDate == null) ? 1 : 0)
                .ThenByDescending(s => s.Id)
                .Skip(pageIndex * count)
                .Take(count).ToList()
                .OrderByDescending(s => (s.WorkerId == currentUserService.User.Id && s.DeliveredDate == null) ? 1 : 0)
                .ThenByDescending(s => s.Id)
                .Select(s => s.ConvertToOrderDTO())
                .ToList();
        }

        [HttpPost("change-status")]
        [Authorize(Roles = "Worker")]
        public OrderDTO ChangeStatus([FromBody] ChangeOrderStatusRequest request)
        {
            var order = orderRepository.Get(request.OrderId);

            if (order == null)
                throw new CustomException("Order not found", 404);

            if (order.WorkerId != null && order.WorkerId != currentUserService.User.Id)
                throw new CustomException("Not permitted", 403);

            order.WorkerId = currentUserService.User.Id;

            order.Status = request.OrderStatus;

            order.Worker = currentUserService.UserEntity;

            if (order.Status == Enums.OrderStatus.Delivired)
            {
                order.DeliveredDate = DateTime.UtcNow;
            }

            orderRepository.SaveChanges();

            return order.ConvertToOrderDTO();
        }


    }
}
