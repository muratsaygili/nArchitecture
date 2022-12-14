using Application.Features.GithubSocials.Dto;
using Application.Features.GithubSocials.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Features.Auths.Rules;

namespace Application.Features.GithubSocials.Commands.CreateGithubSocial
{
    public class CreateGithubSocialCommand : IRequest<CreateGithubSocialDto>
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }

        public class CreateGithubSocialCommandHandler : IRequestHandler<CreateGithubSocialCommand, CreateGithubSocialDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubSocialRepository _gitHubSocialRepository;
            private readonly GithubSocialBusinessRules _githubSocialBusinessRules;
            private readonly AuthBusinessRules _userBusinessRules;
            public CreateGithubSocialCommandHandler(AuthBusinessRules userBusinessRules,IMapper mapper, IGithubSocialRepository gitHubSocialRepository, GithubSocialBusinessRules githubSocialBusinessRules)
            {
                _mapper = mapper;
                _gitHubSocialRepository = gitHubSocialRepository;
                _userBusinessRules = userBusinessRules;
                _githubSocialBusinessRules = githubSocialBusinessRules;
            }

            public async Task<CreateGithubSocialDto> Handle(CreateGithubSocialCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExist(request.UserId);
                await _githubSocialBusinessRules.GithubSocialShouldExistWhenGithubProfileInsert(request.GithubUrl);


                GithubSocial githubProfile = _mapper.Map<GithubSocial>(request);
                githubProfile.IsActive= true;
                var createdGithubProfile= await _gitHubSocialRepository.AddAsync(githubProfile);

                CreateGithubSocialDto createdGitHubProfileDto = _mapper.Map<CreateGithubSocialDto>(createdGithubProfile);

                return createdGitHubProfileDto;
            }
        }
    }
}
