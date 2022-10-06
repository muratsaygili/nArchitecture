using Application.Features.GithubSocials.Commands.CreateGithubSocial;
using Application.Features.GithubSocials.Commands.DeleteGithubSocial;
using Application.Features.GithubSocials.Commands.UpdateGithubSocial;
using Application.Features.GithubSocials.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/social-github")]
    [ApiController]
    public class GithubSocialController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubSocialCommand createGithubSocialCommand)
        {
            CreateGithubSocialDto result = await Mediator.Send(createGithubSocialCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubSocialCommand updateGithubSocialCommand)
        {
            UpdatedGithubSocialDto result = await Mediator.Send(updateGithubSocialCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubSocialCommand updateGithubSocialCommand)
        {
            var result = await Mediator.Send(updateGithubSocialCommand);
            return Ok(result);
        }
    }
}
