using FluentValidation;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class AddVehicleRequestValidator : AbstractValidator<AddVehicleRequest>
    {
        public AddVehicleRequestValidator()
        {

            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("Please provide a name for Name.");
            RuleFor(s => s.Model).NotNull().NotEmpty().WithMessage("Please provide a name for Model.");
            RuleFor(s => s.Year).Must(s => s > 1930).NotNull().NotEmpty().WithMessage("Please provide a legit production year.");
            RuleFor(s => s.Brand).NotNull().NotEmpty().WithMessage("Please provide a name for Brand.");
        }
    }
}
