using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Rules;
using Application.Features.ProgrammingLanguages.Rules;
using Core.Application.Pipelines.Authorization;
using Application.Features.GithubSocials.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Technologies.Rules;
using Application.Services.AuthService;
using Application.Features.UserOperationClaims.Rules;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<GithubSocialBusinessRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<UserOperationClaimBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();

            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();
            
            return services;

        }
    }
}