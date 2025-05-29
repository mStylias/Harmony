using Harmony.EntityFrameworkCore.Abstractions;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;

namespace Harmony.EntityFrameworkCore.Extensions;

public static class RepositoryExtensions
{
    public static IQueryable<TDto> ProjectTo<TDto, TEntity>(
        this IQueryable<TEntity> queryable, 
        IEntityMapper<TEntity, TDto> mapper) 
        where TEntity : class, IEntity
    {
        return mapper.ProjectToDto(queryable);
    }
}