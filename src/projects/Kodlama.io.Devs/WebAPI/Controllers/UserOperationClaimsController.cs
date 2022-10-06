using Application.Features.UserOperationClaims.Commands.Add;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/user-operation-claims")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetUserOperationClaimsListQuery request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserOperationClaimCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
