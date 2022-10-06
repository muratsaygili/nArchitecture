using Application.Features.Auths.Helpers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update
{
    public sealed class UpdateOperationClaimCommand : IRequest<bool>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString() };
    }

    public sealed class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, bool>
    {
        private readonly OperationClaimBusinessRules _businessRules;
        private readonly IOperationClaimRepository _repository;

        public UpdateOperationClaimCommandHandler(OperationClaimBusinessRules businessRules, IOperationClaimRepository repository)
        {
            _businessRules = businessRules;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var operationClaim = await _businessRules.OperationClaimMustExistBeforeUpdatedOrDeleted(request.Id);

            operationClaim.Name = request.Name;

            await _repository.UpdateAsync(operationClaim);

            return true;

        }
    }
}
