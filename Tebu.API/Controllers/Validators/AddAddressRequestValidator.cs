using FluentValidation;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class AddAddressRequestValidator : AbstractValidator<AddAddressRequest>
    {
        public AddAddressRequestValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("Please provide a name for Name.");
            RuleFor(s => s.FullAdress).NotNull().NotEmpty().WithMessage("Please provide a name for Full Adress.");
            RuleFor(s => s.City).NotNull().NotEmpty().WithMessage("Please provide a name for City.");
            RuleFor(s => s.District).NotNull().NotEmpty().WithMessage("Please provide a name for District.");
        }

    }
}
