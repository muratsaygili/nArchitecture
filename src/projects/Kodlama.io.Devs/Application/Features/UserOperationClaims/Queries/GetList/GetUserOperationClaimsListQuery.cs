using Application.Features.Auths.Helpers;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList
{

    public sealed class GetUserOperationClaimsListQuery:IRequest<GetUserOperationClaimsListQueryResponse>,ISecuredRequest
    {
        public int UserId { get; set; }
        public string[] Roles { get; } = { RoleTypes.Admin.ToString(), RoleTypes.Moderator.ToString() };
    }

    public sealed class GetUserOperationClaimsListQueryHandler : IRequestHandler<GetUserOperationClaimsListQuery, GetUserOperationClaimsListQueryResponse>
    {
        private readonly UserOperationClaimBusinessRules _businessRules;

        public GetUserOperationClaimsListQueryHandler(UserOperationClaimBusinessRules businessRules)
        {
            _businessRules = businessRules;
        }

        public async Task<GetUserOperationClaimsListQueryResponse> Handle(GetUserOperationClaimsListQuery request, CancellationToken cancellationToken)
        {
            var user = await _businessRules.UserAndOperationClaimsMustExistBeforeListed(request.UserId);

            var userOperationClaimsDtoList = user.UserOperationClaims.Select(e => new UserOperationClaimDto
            {
                Id=e.Id,
                OperationClaimId=e.OperationClaimId,
                OperationClaimName=e.OperationClaim.Name
            }).ToArray();


            var response = new GetUserOperationClaimsListQueryResponse
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                UserOperationClaimsDtoList = userOperationClaimsDtoList
            };

            return response;
        }
    }

    public sealed class GetUserOperationClaimsListQueryResponse:GetUserOperationClaimsListModel
    {
    }
}
