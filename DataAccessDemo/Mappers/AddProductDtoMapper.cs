using System.Globalization;
using DataAccessDemo.Dtos;
using DataAccessDemo.Entities;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;
using Riok.Mapperly.Abstractions;

namespace DataAccessDemo.Mappers;

// TODO: Check this: https://mapperly.riok.app/docs/configuration/user-implemented-methods/#use-external-mappings
// for creating the harmony auto localization mappings
[Mapper(AutoUserMappings = false)]
public partial class AddProductDtoMapper : IEntityMapper<Product, AddProductDto>
{
    [MapperIgnoreTarget(nameof(Product.Localizations))]
    public partial Product ToEntity(AddProductDto dto);
    
    public partial AddProductDto ToDto(Product entity);
    public partial IQueryable<AddProductDto> ProjectToDto(IQueryable<Product> entityQueryable);
}