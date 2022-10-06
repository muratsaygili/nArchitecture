using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Profiles
{
    public sealed class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateOperationClaimCommand, OperationClaim>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();

            CreateMap<IPaginate<OperationClaim>, GetOperationClaimListQueryResponse>().ReverseMap();
            //CreateMap<IList<OperationClaim>, GetOperationClaimListQueryResponse>().ReverseMap();
        }
    }
}
