
using Application.Features.GithubSocials.Commands.DeleteGithubSocial;
using FluentValidation;

namespace Application.Features.GithubSocials.Commands.UpdateGithubSocial
{
    public class DeleteGithubSocialValidator : AbstractValidator<DeleteGithubSocialCommand>
    {
        public DeleteGithubSocialValidator()
        {
            RuleFor(c => c.Id).NotNull();
        }
    }
}
