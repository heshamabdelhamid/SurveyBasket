using SurveyBasket.Services;
using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Persistence;

namespace SurveyBasket;

public static class DependencyInjection
{
    
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services
            .AddMapster()
            .AddScoped<IPollService, PollService>()
            .AddFluentValidationConfig()
            .AddDatabase(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        //Add mapster
        //var MappingConfig = TypeAdapterConfig.GlobalSettings;
        //MappingConfig.Scan (Assembly.GetExecutingAssembly());
        //services.AddSingleton<IMapper>(new Mapper(MappingConfig));
        return services;
    }
    
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()) .AddFluentValidationAutoValidation();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Could not find connection string");

        services.AddDbContext<SurveyBasketDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
    
}