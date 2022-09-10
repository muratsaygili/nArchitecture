using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.MailCanNotBeDuplicatedWhenInserted(request.Email);
                var registeredUser = _mapper.Map<User>(request);
                registeredUser.Status = true;
                await _userBusinessRules.PasswordHashGenerator(registeredUser,request.Password);
                await _userRepository.AddAsync(registeredUser);
                return new()
                {
                    Message ="User created successfully"
                };

            }
        }
    }
}
