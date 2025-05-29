using DataAccessDemo.Dtos;
using DataAccessDemo.Entities;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;
using Riok.Mapperly.Abstractions;

namespace DataAccessDemo.Mappers;

[Mapper]
public partial class StoreDtoMapper : IEntityMapper<Store, StoreDto>
{
    public partial Store ToEntity(StoreDto dto);
    
    [MapProperty(nameof(ProductLocalization.Name), nameof(ProductDto.Name))]
    public partial StoreDto ToDto(Store entity);
    
    public partial IQueryable<StoreDto> ProjectToDto(IQueryable<Store> entityQueryable);
    
    private static ProductDto MapProductToDto(Product product)
    {
        return new ProductDto
        {
            Name = product.Localizations.FirstOrDefault()?.Name ?? string.Empty,
            Price = product.Price,
        };
    }
}
