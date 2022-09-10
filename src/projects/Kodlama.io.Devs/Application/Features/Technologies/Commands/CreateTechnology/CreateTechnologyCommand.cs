using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int ApplicationLanguageId { get; set; }
        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;
            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.TechnologyCanNotBeDuplicatedWhenInserted(request.Name);
                await _businessRules.ProgrammingLanguageIsExistWhenInserted(request.ApplicationLanguageId);
                Technology technology = _mapper.Map<Technology>(request);
                technology.IsActive= true;
                Technology createdProgrammingEntity = await _technologyRepository.AddAsync(technology);
                var programmingLanguageEntityDto = _mapper.Map<CreatedTechnologyDto>(createdProgrammingEntity);
                return programmingLanguageEntityDto;
            }
        }
    }
}
