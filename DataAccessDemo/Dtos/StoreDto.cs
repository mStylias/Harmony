using DataAccessDemo.Entities;

namespace DataAccessDemo.Dtos;

public class StoreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductDto>? Products { get; set; }
}
