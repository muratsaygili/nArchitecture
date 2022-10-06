using Application.Features.UserOperationClaims.Commands.Add;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Profiles
{
    public sealed class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddUserOperationClaimCommand,UserOperationClaim>();
        }
    }
}
