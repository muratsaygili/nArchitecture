using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguageById;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            //queries
            CreateMap<IPaginate<ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap();
            CreateMap<ProgrammingLanguage, ProgrammingLanguageListDto>().ReverseMap();
            CreateMap<ProgrammingLanguage, ProgrammingLanguageGetByIdDto>().ReverseMap();

            //commands
            CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap();

            CreateMap<ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageDto>().ReverseMap();

            CreateMap<ProgrammingLanguage, DeleteProgrammingLanguageByIdCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageDto>().ReverseMap();


        }
    }
}
