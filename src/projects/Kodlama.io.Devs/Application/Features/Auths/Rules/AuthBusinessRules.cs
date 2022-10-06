using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Mail already exists");

        }
        public void UserCredentialsMustMatchBeforeLogin(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                throw new BusinessException("Check your credentials");
            }
        }

        public async Task<User> UserShouldExistBeforeLogin(string email)
        {
            var user = await _userRepository.Query().Include(p => p.UserOperationClaims).ThenInclude(u => u.OperationClaim)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new BusinessException("Check your credentials");
            }

            return user;
        }
        public async Task MailCanNotBeDuplicatedWhenInserted(string email)
        {
            var result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Items.Any()) throw new BusinessException("Mail is exist");
        }

        public async Task UserShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("Requested User does not exist");
        }

        public async Task PasswordCheck(User user, string password)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("Password is not valid");
        }

        public async Task PasswordHashGenerator(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

        }

        public async Task UserShouldExist(int userId)
        {
            var user = await _userRepository.GetAsync(a => a.Id == userId);
            if (user == null) throw new BusinessException("User is not exist");
        }
    }
}
