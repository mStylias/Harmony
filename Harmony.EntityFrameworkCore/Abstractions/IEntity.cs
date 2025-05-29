namespace Harmony.EntityFrameworkCore.Abstractions;

public interface IEntity
{
}

public interface IEntity<TId> : IEntity
{
    TId Id { get; set; }
}