using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Rules
{
    public sealed class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        internal async Task ClaimNameCannotBeDuplicatedWhenInserted(string name)
        {
            if (await _operationClaimRepository.GetAsync(e => e.Name == name) != null)
                throw new BusinessException("Operation claim exists");
        }

        internal async Task<OperationClaim> OperationClaimMustExistBeforeUpdatedOrDeleted(int id)
        {
            var operationClaim = await _operationClaimRepository.GetAsync(e => e.Id == id);

            if (operationClaim == null) throw new BusinessException("Operation claim does not exist");

            return operationClaim;
        }
    }
}
