using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Create
{
    public sealed class CreateOperationClaimValidator:AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimValidator()
        {
            RuleFor(e => e.Name).NotEmpty().NotEqual("string").WithMessage("{PropertyName} can not be equal to string");
        }
    }
}
