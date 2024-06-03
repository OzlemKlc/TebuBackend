using FluentValidation;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(s => s.Phone).NotEmpty().NotNull().WithMessage("Provide your Phone Number and Password");
            RuleFor(s => s.Password).NotEmpty().NotNull().WithMessage("Provide your Phone Number and Password");
        }
    }
}
