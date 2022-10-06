using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Rules
{
    public sealed class UserOperationClaimBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserAndOperationClaimMustExistBeforeAdded(int userId, int operationClaimId)
        {
            var user = await _userRepository.GetAsync(e => e.Id == userId, enableTracking: false);
            if (user == null) throw new BusinessException("User does not exist");

            var operationClaim = await _operationClaimRepository.GetAsync(e => e.Id == operationClaimId, enableTracking: false);

            if (operationClaim==null) throw new BusinessException("Operation claim does not exist");
        }

        public async Task<UserOperationClaim> UserAndOperationClaimMustExistBeforeDeleted(int id)
        {
            var userOperationClaim = await _userOperationClaimRepository.GetAsync(e => e.Id == id);

            if (userOperationClaim == null) throw new BusinessException("User operation claim does not exist");
            return userOperationClaim;
        }

        public async Task UserOperationClaimCannotBeDuplicatedBeforeAdded(int userId, int operationClaimId)
        {
            if (await _userOperationClaimRepository.GetAsync(e => e.UserId == userId && e.OperationClaimId == operationClaimId) != null)
                throw new BusinessException("User already has this operation claim");
        }

        public async Task<User> UserAndOperationClaimsMustExistBeforeListed(int userId)
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId, enableTracking: false, include: u => u.Include(u => u.UserOperationClaims).ThenInclude(uop=>uop.OperationClaim));

            if (user == null) throw new BusinessException("User does not exist");

            return user;
        }
    }
}
