using DataAccessDemo;
using DataAccessDemo.Dtos;
using DataAccessDemo.Entities;
using DataAccessDemo.Mappers;
using DataAccessDemo.Persistence;
using Harmony.EntityFrameworkCore;
using Harmony.EntityFrameworkCore.Abstractions;
using Harmony.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Mvc;
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

app.MapGet("test", async (IRepository<Store> storeRepo, IRepository<Product> productRepo, [FromQuery] string locale) =>
    {
        var store = new Store
        {
            Id = 1,
            Name = "Test Store",
            Location = "A location",
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Localizations = new List<ProductLocalization>()
                    {
                        new ProductLocalization()
                        {
                            Id = 1,
                            Locale = "en-US",
                            Name = "Apples",
                            Description = "Some apples",
                        },
                    },
                    Price = 20,
                },
            },
        };

        var storeDto = new StoreDto
        {
            Id = 2,
            Name = "Test Store DTO",
        };

        var mapper = new StoreDtoMapper();
        
        var convertedDto = mapper.ToDto(store);
        var convertedStore = mapper.ToEntity(storeDto);
        var projectedStore = storeRepo
            .Where(r => r.Id == 2)
            .Include(r => r.Products)
            .ThenInclude(p => p.Localizations.Where(l => l.Locale == locale))
            .ProjectTo(storeRepo.GetMapperFor<StoreDto>())
            .FirstOrDefault();

        // await storeRepo.AddAsync(convertedDto);
        var addProductDto = new AddProductDto
        {
            LocalizedValues = new List<AddProductDtoLocalization>
            {
                new AddProductDtoLocalization
                {
                    Name = "Test Product",
                    Description = "This is a test product",
                },
            },
            Price = 10,
            StoreId = 1,
        };

        var productDtoMapper = productRepo.GetMapperFor<AddProductDto>();
        var product = productDtoMapper.ToEntity(addProductDto);
        
        await productRepo.AddAsync(product);
        
        // await productRepo.AddAsync(addProductDto);
        
        return Results.Ok(product);
        
        // return Results.Ok(new { convertedDto, convertedStore, projectedStore });
    })
    .WithName("Test");

await app.RunAsync().ConfigureAwait(true);