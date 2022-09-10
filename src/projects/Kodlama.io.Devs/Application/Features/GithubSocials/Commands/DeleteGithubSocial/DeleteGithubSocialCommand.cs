using Application.Features.GithubSocials.Dto;
using Application.Features.GithubSocials.Rules;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.Features.GithubSocials.Commands.DeleteGithubSocial
{
    public class DeleteGithubSocialCommand : IRequest<DeleteGithubSocialDto>
    {
        public int Id { get; set; }
        public class DeleteGithubSocialCommandHandler : IRequestHandler<DeleteGithubSocialCommand, DeleteGithubSocialDto>
        {
            private readonly IGithubSocialRepository _githubSocialRepository;
            private readonly IMapper _mapper;
            private readonly GithubSocialBusinessRules _businessRules;
            public DeleteGithubSocialCommandHandler(IGithubSocialRepository githubSocialRepository, IMapper mapper, GithubSocialBusinessRules businessRules)
            {
                _githubSocialRepository = githubSocialRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<DeleteGithubSocialDto> Handle(DeleteGithubSocialCommand request, CancellationToken cancellationToken)
            {
                var githubSocialToBeDeleted = await _githubSocialRepository.GetAsync(a => a.Id == request.Id);
                _businessRules.GithubSocialShouldExistWhenRequested(githubSocialToBeDeleted);
                githubSocialToBeDeleted.IsActive = false;

                var deletedProgrammingLangugage = await _githubSocialRepository.UpdateAsync(githubSocialToBeDeleted);
                var deletedTechnologyEntityDto = _mapper.Map<DeleteGithubSocialDto>(deletedProgrammingLangugage);

                return deletedTechnologyEntityDto;
            }
        }
    }
}
