using DataAccessDemo.Dtos;
using DataAccessDemo.Entities;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;
using Riok.Mapperly.Abstractions;

namespace DataAccessDemo.Mappers;

[Mapper]
public partial class AddProductDtoLocalizationMapper : IEntityMapper<ProductLocalization, AddProductDtoLocalization>
{
    public partial ProductLocalization ToEntity(AddProductDtoLocalization dto);

    public partial AddProductDtoLocalization ToDto(ProductLocalization entity);

    public partial IQueryable<AddProductDtoLocalization> ProjectToDto(IQueryable<ProductLocalization> entityQueryable);
}