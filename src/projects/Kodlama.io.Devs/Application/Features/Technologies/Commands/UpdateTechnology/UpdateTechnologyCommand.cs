using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;
            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                var technologyBeUpdated = await _technologyRepository.GetAsync(b => b.Id == request.Id);

                _businessRules.TechnologyShouldExistWhenRequested(technologyBeUpdated);
                technologyBeUpdated.Name = request.Name;

                var updatedTechnology = await _technologyRepository.UpdateAsync(technologyBeUpdated);
                var mappedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return mappedTechnologyDto;
            }
        }
    }
}
