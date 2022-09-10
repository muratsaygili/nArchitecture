
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeleteTechnologyDto>().ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>()
                .ForMember(c => c.ApplicationLanguageId, opt => opt.MapFrom(a => a.ProgrammingLanguageId))
                .ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();

            CreateMap<Technology, TechnologyListDto>()
             .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(a => a.ProgrammingLanguage.Name));

            CreateMap<Technology, TechnologyByIdDto>()
          .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(a => a.ProgrammingLanguage.Name));
        }
    }
}
