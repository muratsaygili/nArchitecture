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

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand : IRequest<DeleteTechnologyDto>
    {
        public int Id { get; set; }
        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeleteTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;
            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<DeleteTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                var technologyDtoBeDeleted = await _technologyRepository.GetAsync(a => a.Id == request.Id);
                _businessRules.TechnologyShouldExistWhenRequested(technologyDtoBeDeleted);
                technologyDtoBeDeleted.IsActive = false;

                Technology deletedProgrammingLangugage = await _technologyRepository.UpdateAsync(technologyDtoBeDeleted);
                var deletedTechnologyEntityDto = _mapper.Map<DeleteTechnologyDto>(deletedProgrammingLangugage);

                return deletedTechnologyEntityDto;
            }
        }
    }
}
