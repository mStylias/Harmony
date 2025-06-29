namespace Harmony.EntityFrameworkCore.Abstractions.Localization;

public interface ILocalizationEntity<TTargetEntityId> : IEntity<int>
{
    TTargetEntityId TargetEntityId { get; set; }
}