using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
