using Application.Features.Auths.Helpers;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete
{
    public class DeleteUserOperationClaimCommand : IRequest<bool>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString(), RoleTypes.Moderator.ToString() };
    }

    public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, bool>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _businessRules;

        public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules businessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _businessRules = businessRules;
        }

        public async Task<bool> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var userOperationClaim = await _businessRules.UserAndOperationClaimMustExistBeforeDeleted(request.Id);

            await _userOperationClaimRepository.DeleteAsync(userOperationClaim);

            return true;
        }
    }
}
