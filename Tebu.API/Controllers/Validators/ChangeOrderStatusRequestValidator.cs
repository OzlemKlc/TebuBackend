using FluentValidation;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class ChangeOrderStatusRequestValidator : AbstractValidator<ChangeOrderStatusRequest>
    {
        public ChangeOrderStatusRequestValidator()
        {
            RuleFor(s => s.OrderId).NotEmpty().NotNull().WithMessage("Provide an order id");
            RuleFor(s => s.OrderStatus).NotEmpty().NotNull().WithMessage("Provide a legit order status");
        }
    }
}
