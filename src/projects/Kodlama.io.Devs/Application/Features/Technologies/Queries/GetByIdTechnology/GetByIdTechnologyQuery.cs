
using Application.Features.Technologies.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    public class GetByIdTechnologyQuery : IRequest<TechnologyByIdDto>
    {

        public int Id { get; set; }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyByIdDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }
            public async Task<TechnologyByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                var technology = await _technologyRepository.GetAsync(a => a.Id == request.Id && a.IsActive);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);
                TechnologyByIdDto mappedtechnologyModel = _mapper.Map<TechnologyByIdDto>(technology);
                return mappedtechnologyModel;
            }
        }
    }
}
