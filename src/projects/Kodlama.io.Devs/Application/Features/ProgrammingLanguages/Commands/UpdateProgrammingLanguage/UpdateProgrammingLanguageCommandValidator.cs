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
using FluentValidation;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommandValidator:AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        public UpdateProgrammingLanguageCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Value cannot be empty");
        }
    }
}
