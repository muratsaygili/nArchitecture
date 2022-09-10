using Application.Features.GithubSocials.Dto;
using Application.Features.GithubSocials.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubSocials.Commands.CreateGithubSocial
{
    public class UpdateGithubSocialCommand : IRequest<UpdatedGithubSocialDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubUrl { get; set; }

        public class UpdateGithubSocialCommandHandler : IRequestHandler<UpdateGithubSocialCommand, UpdatedGithubSocialDto>
        {
            private readonly IGithubSocialRepository _socialRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;
            private readonly GithubSocialBusinessRules _githubSocialBusinessRules;
            public UpdateGithubSocialCommandHandler(UserBusinessRules userBusinessRules, IGithubSocialRepository socialRepository, IMapper mapper, GithubSocialBusinessRules githubSocialBusinessRules)
            {
                _socialRepository = socialRepository;   
                _mapper = mapper;
                _githubSocialBusinessRules = githubSocialBusinessRules;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UpdatedGithubSocialDto> Handle(UpdateGithubSocialCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExist(request.UserId);
                await _githubSocialBusinessRules.GithubSocialShouldExistWhenGithubProfileInsert(request.GithubUrl);
                var mappedGithubSocial = _mapper.Map<GithubSocial>(request);
                var updatedGithubSocial = await _socialRepository.UpdateAsync(mappedGithubSocial);
                var mappedUpdatedGithubSocial = _mapper.Map<UpdatedGithubSocialDto>(updatedGithubSocial);
                return mappedUpdatedGithubSocial;
            }
        }
    }
}
