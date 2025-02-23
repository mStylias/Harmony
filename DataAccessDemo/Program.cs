using DataAccessDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection();

app.MapGet("test", () =>
    {
        return "test";
    })
    .WithName("Test");

await app.RunAsync().ConfigureAwait(true);