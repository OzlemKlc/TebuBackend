using FluentValidation;
using System.Text.RegularExpressions;
using Tebu.API.Controllers.DTO;

namespace Tebu.API.Controllers.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(s => s.PhoneNumber).NotEmpty().NotNull().Matches(new Regex(@"^(((\+|00)?(90)|0)[-| ]?)?((5\d{2})[-| ]?(\d{3})[-| ]?(\d{2})[-| ]?(\d{2}))$")).WithMessage("Provide a Phone Number");
            RuleFor(s => s.Password).NotEmpty().NotNull().WithMessage("Provide a Password");
            RuleFor(s => s.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Provide an Email");
            RuleFor(s => s.Name).NotEmpty().NotNull().WithMessage("Provide a Name");
            RuleFor(s => s.Surname).NotEmpty().NotNull().WithMessage("Provide a Surname");
        }
    }
}
