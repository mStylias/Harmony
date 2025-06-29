using Harmony.EntityFrameworkCore.Abstractions.Localization;

namespace DataAccessDemo.Dtos;

public class AddProductDto : IDtoWithLocalization<int, AddProductDtoLocalization>
{
    public ICollection<AddProductDtoLocalization> LocalizedValues { get; set; }
    public decimal Price { get; set; }
    public int StoreId { get; set; }
}

public class AddProductDtoLocalization
{
    public string Locale { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
