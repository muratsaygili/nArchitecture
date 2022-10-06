using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Delete
{
    public sealed class DeleteOperationClaimCommandValidator:AbstractValidator<DeleteOperationClaimCommand>
    {
        public DeleteOperationClaimCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotEqual(0).WithMessage("{PropertyName} can not be equal to 0");
        }
    }
}
