using Application.Features.GithubSocials.Commands.CreateGithubSocial;
using FluentValidation;

namespace Application.Features.GithubSocials.Commands.UpdateGithubSocial
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
