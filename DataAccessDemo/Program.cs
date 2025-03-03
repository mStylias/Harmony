using DataAccessDemo;
using DataAccessDemo.Entities;
using DataAccessDemo.Persistence;
using Harmony.EntityFrameworkCore;
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

app.MapGet("test", async (IRepository<Product> productRepo, IRepository<ProductLocalization> localRepo) =>
    {
        var test = await productRepo
            .Join(
                localRepo.DbSet,
                p => p.NameLocaleId,
                l => l.LocalizationEntryId,
                (p, l) => new { p, l });
    })
    .WithName("Test");

await app.RunAsync().ConfigureAwait(true);