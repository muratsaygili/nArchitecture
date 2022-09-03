using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand:IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }

        public class CreateProgrammingLanguageCommandHandler:IRequestHandler<CreateProgrammingLanguageCommand,CreatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }
            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgrammingLanguageCannotBeDuplicatedWhenInserting(request.Name);

                var mappedEntity = _mapper.Map<ProgrammingLanguage>(request);
                var createdEntity =await _repository.AddAsync(mappedEntity);
                var createdEntityDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdEntity);
                return createdEntityDto;
            }
        }
    }
}
