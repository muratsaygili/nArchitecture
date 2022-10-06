using Application.Features.Auths.Helpers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Delete
{
    public sealed class DeleteOperationClaimCommand:IRequest<bool>,ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString() };
    }

    public sealed class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, bool>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _businessRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules businessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _businessRules = businessRules;
        }

        public async Task<bool> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var operationClaim = await _businessRules.OperationClaimMustExistBeforeUpdatedOrDeleted(request.Id);

            await _operationClaimRepository.DeleteAsync(operationClaim);

            return true;
        }
    }

}
