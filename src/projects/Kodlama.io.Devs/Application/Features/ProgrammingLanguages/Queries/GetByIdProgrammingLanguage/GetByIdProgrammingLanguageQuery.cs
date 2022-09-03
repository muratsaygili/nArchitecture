using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage
{
    public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }
            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                var data = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id);

                _businessRules.ProgrammingLanguageShouldExistWhenRequested(data);
                
                var mappedData = _mapper.Map<ProgrammingLanguageGetByIdDto>(data);
                return mappedData;
            }
        }
    }
}
