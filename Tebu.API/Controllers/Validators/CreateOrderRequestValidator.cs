using FluentValidation;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(s => s.OrderType).NotEmpty().NotNull().WithMessage("Provide an order id");
            RuleFor(s => s.VehicleId).NotEmpty().NotNull().WithMessage("Provide an order id");
            RuleFor(s => s.AddressId).NotEmpty().NotNull().WithMessage("Provide an order id");
        }
    }
}
