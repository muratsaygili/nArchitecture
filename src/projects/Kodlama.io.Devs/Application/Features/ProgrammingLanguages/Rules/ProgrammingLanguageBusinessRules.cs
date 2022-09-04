using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage entity)
        {
            if (entity == null) throw new BusinessException("Programming language does not exist");
        }

        public async Task ProgrammingLanguageCannotBeDuplicatedWhenInserting(string name)
        {
            var data = await _programmingLanguageRepository.GetAsync(x => x.Name == name);
            if (data != null) throw new BusinessException("Programming language cannot be duplicated when inserting");
        }
        public async Task ProgrammingLanguageCannotBeDuplicatedWhenUpdating(int id,string name)
        {
            //we are looking for another entity with given name and not equal with given id, entity name can be exactly old name
            var data = await _programmingLanguageRepository.GetListAsync(x => x.Id != id && x.Name == name, enableTracking: false);
            if (data.Items.Any()) throw new BusinessException("Programming language cannot be duplicated when inserting");
        }
        public async Task ProgrammingLanguageShouldExistWithId(int id)
        {
            var data = await _programmingLanguageRepository.GetListAsync(x => x.Id == id, enableTracking: false);
            
            if (!data.Items.Any()) throw new BusinessException("Programming language doesnt exist with id "+id);
        }
    }
}
