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

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand:IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }
            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgrammingLanguageShouldExistWithId(request.Id);
                await _businessRules.ProgrammingLanguageCannotBeDuplicatedWhenUpdating(request.Id,request.Name);

                var mappedEntity = _mapper.Map<ProgrammingLanguage>(request);
                var updatedEntity =await _repository.UpdateAsync(mappedEntity);
                var updatedEntityDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedEntity);
                return updatedEntityDto;
            }
        }
    }
}
