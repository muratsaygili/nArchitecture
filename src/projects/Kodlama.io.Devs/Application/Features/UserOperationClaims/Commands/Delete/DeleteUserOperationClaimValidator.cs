using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Delete
{
    public sealed class DeleteUserOperationClaimValidator:AbstractValidator<DeleteUserOperationClaimCommand>
    {
        public DeleteUserOperationClaimValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotEqual(0).WithMessage("{PropertyName} cannot be equal to 0");
        }
    }
}
