using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand:UserForLoginDto,IRequest<LoggedInDto>
    {
    }

    public class LoginCommandHandler:IRequestHandler<LoginCommand,LoggedInDto>
    {
        private readonly AuthBusinessRules _userBusinessRules;
        private readonly ITokenHelper _tokenHelper;

        public LoginCommandHandler(AuthBusinessRules userBusinessRules, ITokenHelper tokenHelper, IUserRepository userRepository)
        {
            _userBusinessRules = userBusinessRules;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoggedInDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
 
            var user = await _userBusinessRules.UserShouldExistBeforeLogin(request.Email);

            _userBusinessRules.UserCredentialsMustMatchBeforeLogin(request.Password, user.PasswordHash, user.PasswordSalt);
            

            var token = _tokenHelper.CreateToken(user, user.UserOperationClaims.Select(p => p.OperationClaim).ToList());
            return new() { AccessToken = token };
        }
    }
}
