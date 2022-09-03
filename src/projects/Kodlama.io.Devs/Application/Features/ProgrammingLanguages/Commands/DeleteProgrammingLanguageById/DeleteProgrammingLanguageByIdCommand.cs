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

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguageById
{
    public class DeleteProgrammingLanguageByIdCommand:IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }

        public class DeleteProgrammingLanguageByIdCommandHandler:IRequestHandler<DeleteProgrammingLanguageByIdCommand,DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public DeleteProgrammingLanguageByIdCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }
            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageByIdCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgrammingLanguageShouldExistWithId(request.Id);

                var mappedEntity = _mapper.Map<ProgrammingLanguage>(request);
                var deletedEntity=await _repository.DeleteAsync(mappedEntity);
                var mappedDto = _mapper.Map<DeletedProgrammingLanguageDto>(deletedEntity);
                return mappedDto;
            }
        }
    }
}
