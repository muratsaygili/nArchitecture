using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginUser
{
    
     public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                var userData = await _userRepository.GetAsync(p => p.Email == request.Email);
                await _userBusinessRules.UserShouldExistWhenRequested(userData);
                await _userBusinessRules.PasswordCheck(userData, request.Password);

                var accessToken = _tokenHelper.CreateToken(userData, new List<OperationClaim>());
                var mappedAccessToken = _mapper.Map<LoginUserDto>(accessToken);

                return mappedAccessToken;

            }
        }
    }
}
