using DataAccessDemo;
using DataAccessDemo.Entities;
using DataAccessDemo.Persistence;
using Harmony.EntityFrameworkCore;
using Harmony.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
})
.AddHarmonyEfCore<AppDbContext>(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection();

app.MapGet("test", async (IRepository<Product> productRepo) =>
    {
        return string.Empty;
    })
    .WithName("Test");

await app.RunAsync().ConfigureAwait(true);