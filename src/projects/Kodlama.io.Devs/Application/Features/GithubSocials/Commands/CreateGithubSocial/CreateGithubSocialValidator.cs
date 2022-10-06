using FluentValidation;
using Application.Features.GithubSocials.Commands.UpdateGithubSocial;

namespace Application.Features.GithubSocials.Commands.CreateGithubSocial
{
    public class UpdateGithubSocialValidator : AbstractValidator<UpdateGithubSocialCommand>
    {
        public UpdateGithubSocialValidator()
        {
            RuleFor(c => c.GithubUrl).NotEmpty();
            RuleFor(c => c.UserId).NotNull();
        }
    }
}
