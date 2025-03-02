using DataAccessDemo;
using DataAccessDemo.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection();

app.MapGet("test", (AppDbContext dbContext) =>
    {
        return "test";
    })
    .WithName("Test");

await app.RunAsync().ConfigureAwait(true);