using Microsoft.OpenApi.Models;

namespace DataAccessDemo;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddSwaggerServices();

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

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(opts =>
        {
            const string title = "Harmony Ef core API";
            const string description = "A set of example endpoints utilizing harmony ef core";

            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = $"{title} v1",
                Description = description,
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
}