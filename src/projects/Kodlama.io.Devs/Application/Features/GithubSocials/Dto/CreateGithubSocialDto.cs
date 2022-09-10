using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubSocials.Dto
{
    public class CreateGithubSocialDto
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
    }
}
