using Application.Abstractions.Data;
using Domain.Ingredients;
using Domain.Tags;
using Infrastructure.Data;
using Infrastructure.Outbox;
using Infrastructure.Repositories;
using Mediator.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Primitives.Guards;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services,
        string connectionString)
    {
        services.AddMediator(typeof(DependencyInjection).Assembly);

        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<AppReadDbContext>(
            options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddDbContext<AppWriteDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppWriteDbContext>());

        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
    }
}
