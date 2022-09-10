
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _technologyRepository = technologyRepository;
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task TechnologyCanNotBeDuplicatedWhenInserted(string name)
        {
            var result = await _technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }

        public async Task ProgrammingLanguageIsExistWhenInserted(int programmingLanguageId)
        {
            var result = await _programmingLanguageRepository.GetAsync(b => b.Id == programmingLanguageId);
            if (result==null) throw new BusinessException("ProgrammingLanguage not exists.");
        }

        public async Task TechnologyShouldExist(int id)
        {
            var result = await _technologyRepository.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException("Technology should exists.");
        }

        public void TechnologyShouldExistWhenRequested(Technology? technology)
        {
            if (technology == null) throw new BusinessException("Requested Technology does not exist");
        }
    }
}
