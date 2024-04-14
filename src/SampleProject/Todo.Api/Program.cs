using Harmony.MinimalApis;
using Todo.Api;
using Todo.Application;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseConfiguredSwagger();
    }

    app.UseHttpsRedirection();
    
    app.UseExceptionHandler(_ => { });
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapApiEndpoints();
    
    app.Run();
}
