using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubSocials.Rules
{
    public class GithubSocialBusinessRules
    {
        private readonly IGithubSocialRepository _githubSocialRepository;
        public GithubSocialBusinessRules(IGithubSocialRepository githubSocialRepository)
        {
            _githubSocialRepository = githubSocialRepository;
        }
        public async Task GithubSocialShouldExistWhenGithubProfileInsert(string url)
        {
            var githubProfile = await _githubSocialRepository.GetAsync(a => a.GithubUrl == url);
            if (githubProfile != null) throw new BusinessException("Github Social is  exist");
        }

        public void GithubSocialShouldExistWhenRequested(GithubSocial githubSocial)
        {
            if (githubSocial == null) throw new BusinessException("Github Social is not exist");
        }
    }
}
