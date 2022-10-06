using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Add
{
    public sealed class AddUserOperationClaimValidator:AbstractValidator<AddUserOperationClaimCommand>
    {
        public AddUserOperationClaimValidator()
        {
            RuleFor(e => e.UserId).NotEmpty().NotEqual(0).WithMessage("{PropertyName} cannot be 0");
            RuleFor(e => e.OperationClaimId).NotEmpty().NotEqual(0).WithMessage("{PropertyName} cannot be 0");
        }
        
    }
}
