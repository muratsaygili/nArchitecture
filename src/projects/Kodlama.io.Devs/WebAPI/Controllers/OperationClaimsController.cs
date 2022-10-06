using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/operation-claims")]
    [ApiController]
    public sealed class OperationClaimsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetOperationClaimListQuery request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOperationClaimCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
