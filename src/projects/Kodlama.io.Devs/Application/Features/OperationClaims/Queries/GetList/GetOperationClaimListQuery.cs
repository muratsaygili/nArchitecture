using Application.Features.Auths.Helpers;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OperationClaims.Queries.GetList
{
    public class GetOperationClaimListQuery : IRequest<GetOperationClaimListQueryResponse>, ISecuredRequest
    {
        public string[] Roles { get; } = { RoleTypes.Admin.ToString() };
    }

    public class GetOperationClaimListQueryHandler : IRequestHandler<GetOperationClaimListQuery, GetOperationClaimListQueryResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetOperationClaimListQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetOperationClaimListQueryResponse> Handle(GetOperationClaimListQuery request, CancellationToken cancellationToken)
        {
            var operationClaims = await _operationClaimRepository.Query().AsNoTracking().ToListAsync(cancellationToken);

            return new GetOperationClaimListQueryResponse
            {
                Items = operationClaims.Select(x => new OperationClaimDto() { Id = x.Id, Name = x.Name }).ToList()
            };
        }
    }

    public class GetOperationClaimListQueryResponse : OperationClaimListModel
    {
    }
}
