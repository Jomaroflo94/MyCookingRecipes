using Application.Abstractions.Behaviors;
using Domain.Tags;
using FluentValidation;
using Mediator.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediator(typeof(DependencyInjection).Assembly, 
            [
                typeof(RequestLoggingPipelineBehavior<,>), 
                typeof(ValidationPipelineBehavior<,>)
            ]);

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly,
            includeInternalTypes: true);

        services.AddScoped<ITagService, TagService>();
    }
}
