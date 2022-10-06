using FluentValidation;

namespace Application.Features.Auths.Commands.Login
{
    public sealed class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(u=>u.Email).NotEmpty().EmailAddress().WithMessage("Please check your email format");

            RuleFor(u => u.Password).NotEmpty().MinimumLength(8).WithMessage("Minimum password length is 8");
        }
    }
}
