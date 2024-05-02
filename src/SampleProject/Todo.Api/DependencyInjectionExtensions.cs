using Harmony.MinimalApis;
using Harmony.MinimalApis.Exceptions;
using Microsoft.OpenApi.Models;
using Todo.Domain.Options;
using Todo.Domain.Options.Validators;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Todo.Api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAppOptions(config)
            .AddApiEndpoints(typeof(DependencyInjectionExtensions).Assembly)
            .AddEndpointsApiExplorer()
            .AddSwaggerServices()
            .AddExceptionHandler<HarmonyGlobalExceptionHandler>();
        
        return services;
    }

    private static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<JwtOptions>()
            .Bind(config.GetRequiredSection(JwtOptions.SectionName))
            .Validate(x => x.Validate())
            .ValidateOnStart();
        
        services.AddOptions<ConnectionStringsOptions>()
            .Bind(config.GetSection(ConnectionStringsOptions.SectionName))
            .Validate(x => x.Validate())
            .ValidateOnStart();
        
        services.AddOptions<RefreshTokenOptions>()
            .Bind(config.GetSection(RefreshTokenOptions.SectionName))
            .Validate(x => x.Validate())
            .ValidateOnStart();
        
        return services;
    }
    
    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(opts =>
        {
            const string title = "Todo API";
            const string description = "This is an api for a todo app.";
            
            opts.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Version = "v1",
                Title = $"{title} v1",
                Description = description
            });
            
            /*opts.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = $"{title} v2",
                Description = description
            });*/
            
            var xmlFile = $"{typeof(DependencyInjectionExtensions).Assembly.GetName().Name}.xml";
            opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        });

        return services;
    }
    
    public static void UseConfiguredSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.DisplayRequestDuration();
        });
    }
}