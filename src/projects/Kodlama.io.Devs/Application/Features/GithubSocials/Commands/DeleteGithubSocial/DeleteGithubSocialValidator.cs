using FluentValidation;

namespace Application.Features.GithubSocials.Commands.DeleteGithubSocial
{
    public class DeleteGithubSocialValidator : AbstractValidator<DeleteGithubSocialCommand>
    {
        public DeleteGithubSocialValidator()
        {
            RuleFor(c => c.Id).NotNull();
        }
    }
}
