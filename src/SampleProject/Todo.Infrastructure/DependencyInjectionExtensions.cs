using System.Diagnostics;
using Harmony.MinimalApis.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Constants;
using Todo.Domain.Entities.Auth;
using Todo.Domain.Errors;
using Todo.Domain.Options;
using Todo.Domain.Rules;
using Todo.Infrastructure.Auth;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Persistence.Repositories;

namespace Todo.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, 
        bool enableSensitiveDataLogging)
    {
        services
            .AddAuthIdentity()
            .AddAuthServices(config)
            .AddPersistence(config, enableSensitiveDataLogging);
        
        return services;
    }
    
    private static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie(x =>
        {
            x.Cookie.Name = Constants.Auth.AccessTokenCookieName;
        })
        .AddJwtBearer(options =>
        {
            var jwtOptions = config.GetRequiredSection(JwtOptions.SectionName)
                .Get<JwtOptions>();
            var refreshTokenOptions = config.GetRequiredSection(RefreshTokenOptions.SectionName)
                .Get<RefreshTokenOptions>();
            
            Debug.Assert(jwtOptions is not null);
            Debug.Assert(refreshTokenOptions is not null);

            options.TokenValidationParameters = RefreshTokenValidator
                .GetTokenValidationParameters(jwtOptions, jwtOptions.Key);
            
            options.IncludeErrorDetails = true;
            
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies[Constants.Auth.AccessTokenCookieName];
                    if (token is not null)
                    {
                        context.Token = token;
                    }
                    
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Headers.Append("WWW-Authenticate", context.Error);
                    context.Response.ContentType = "application/problem+json";

                    // Here you should use whatever logger you have in your project. E.g. if you have Serilog use the 
                    // static logger of Serilog.
                    var response = Errors.Auth.AccessDenied(loggerFactory.CreateLogger("Auth"), 
                        context.Error);
                    
                    response.Log();

                    var result = response.MapToHttpResult();
                    return result.ExecuteAsync(context.HttpContext);
                }
            };
        });
        
        services.AddAuthorization(opts =>
        {
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        
        services.AddScoped<ITokenCreationService, JwtService>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        services.AddScoped<IAuthCookiesService, AuthCookiesService>();
        
        return services;
    }
    
    private static IServiceCollection AddAuthIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = Rules.Auth.RequireConfirmedAccount;
                options.User.RequireUniqueEmail = Rules.Auth.RequireUniqueEmail;
                options.Password.RequireDigit = Rules.Auth.RequireDigit;
                options.Password.RequiredLength = Rules.Auth.RequiredLength;
                options.Password.RequireNonAlphanumeric = Rules.Auth.RequireNonAlphanumeric;
                options.Password.RequireUppercase = Rules.Auth.RequireUppercase;
                options.Password.RequireLowercase = Rules.Auth.RequireLowercase;
            })
            .AddEntityFrameworkStores<AuthDbContext>();
        
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config, 
        bool enableSensitiveDataLogging)
    {
        var connectionStrings = config.GetRequiredSection(ConnectionStringsOptions.SectionName)
            .Get<ConnectionStringsOptions>();
        
        Debug.Assert(connectionStrings is not null);
        
        services.AddDbContext<AuthDbContext> (options =>
        {
            options.UseSqlite(connectionStrings.Default);
            options.EnableSensitiveDataLogging(enableSensitiveDataLogging);
        });

        services.AddScoped<DapperDbContext>();
        
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ITodosRepository, TodosRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        
        return services;
    }
}