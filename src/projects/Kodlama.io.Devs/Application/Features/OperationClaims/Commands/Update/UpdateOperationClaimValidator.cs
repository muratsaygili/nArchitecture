using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Update
{
    public sealed class UpdateOperationClaimValidator:AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotEqual(0).WithMessage("{PropertyName} can not be equal to 0");
            RuleFor(e => e.Name).NotEmpty().NotEqual("string").WithMessage("{PropertyName} can not be equal to string");
        }
    }
}
