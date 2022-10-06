using Application.Features.Auths.Helpers;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Add
{
    public sealed class AddUserOperationClaimCommand : IRequest<bool>, ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString(),RoleTypes.Moderator.ToString() };
    }

    public sealed class AddUserOperationClaimCommandHandler : IRequestHandler<AddUserOperationClaimCommand, bool>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public AddUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules businessRules, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.UserAndOperationClaimMustExistBeforeAdded(request.UserId,request.OperationClaimId);

            await _businessRules.UserOperationClaimCannotBeDuplicatedBeforeAdded(request.UserId,request.OperationClaimId);

            await _userOperationClaimRepository.AddAsync(_mapper.Map<UserOperationClaim>(request));

            return true;
        }
    }

}
