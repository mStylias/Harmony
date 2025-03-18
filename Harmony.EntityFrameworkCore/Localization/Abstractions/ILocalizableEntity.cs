using Harmony.EntityFrameworkCore.Abstractions;

namespace Harmony.EntityFrameworkCore.Localization.Abstractions;

public interface ILocalizableEntity<TPrimaryKey> : IEntity
{
    TPrimaryKey PrimaryKey { get; set; }
}

public interface ILocalizableEntity<TPrimaryKey1, TPrimaryKey2> : IEntity
{
    TPrimaryKey1 PrimaryKey1 { get; set; }
    TPrimaryKey2 PrimaryKey2 { get; set; }
}

public interface ILocalizableEntity<TPrimaryKey1, TPrimaryKey2, TPrimaryKey3> : IEntity
{
    TPrimaryKey1 PrimaryKey1 { get; set; }
    TPrimaryKey2 PrimaryKey2 { get; set; }
    TPrimaryKey3 PrimaryKey3 { get; set; }
}

public interface ILocalizableEntity<TPrimaryKey1, TPrimaryKey2, TPrimaryKey3, TPrimaryKey4> : IEntity
{
    TPrimaryKey1 PrimaryKey1 { get; set; }
    TPrimaryKey2 PrimaryKey2 { get; set; }
    TPrimaryKey3 PrimaryKey3 { get; set; }
    TPrimaryKey4 PrimaryKey4 { get; set; }
}
