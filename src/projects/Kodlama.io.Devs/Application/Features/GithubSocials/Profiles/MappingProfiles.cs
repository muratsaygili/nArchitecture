using Application.Features.GithubSocials.Commands.CreateGithubSocial;
using Application.Features.GithubSocials.Commands.UpdateGithubSocial;
using Application.Features.GithubSocials.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.GithubSocials.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<GithubSocial, CreateGithubSocialDto>().ReverseMap();
            CreateMap<GithubSocial, CreateGithubSocialCommand>().ReverseMap();
            CreateMap<GithubSocial, UpdateGithubSocialCommand>().ReverseMap();
            CreateMap<GithubSocial, DeleteGithubSocialDto>().ReverseMap();
            CreateMap<GithubSocial, UpdatedGithubSocialDto>().ReverseMap();
        }
    }
}
