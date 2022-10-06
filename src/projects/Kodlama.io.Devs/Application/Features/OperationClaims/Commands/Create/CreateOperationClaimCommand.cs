using Application.Features.Auths.Helpers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create
{
    public sealed class CreateOperationClaimCommand : IRequest<bool>,ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString() };
    }

    public sealed class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, bool>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        private readonly IMapper _mapper;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<bool> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
           await _operationClaimBusinessRules.ClaimNameCannotBeDuplicatedWhenInserted(request.Name);

            var operationClaim = _mapper.Map<OperationClaim>(request);

            await _operationClaimRepository.AddAsync(operationClaim);

            return true;
        }
    }
}
