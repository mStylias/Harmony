using Harmony.EntityFrameworkCore.Abstractions;

namespace Harmony.EntityFrameworkCore.Mapping.Abstractions;

public interface IEntityMapper<TEntity, TDto>
    where TEntity : class, IEntity
{
    TEntity ToEntity(TDto dto);
    TDto ToDto(TEntity entity);
    IQueryable<TDto> ProjectToDto(IQueryable<TEntity> entityQueryable);
}