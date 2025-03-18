using System.Globalization;

namespace Harmony.EntityFrameworkCore.Localization;

public static class QueryableLocalizationExtensions
{
    public static IQueryable<TSource> LocalizeWith<TSource>(this IQueryable<TSource> source, CultureInfo cultureInfo)
    {
        return LocalizeWith(source, CultureInfo.GetCultureInfo(cultureInfo.Name));
    }

    public static IQueryable<TSource> LocalizeWith<TSource>(this IQueryable<TSource> source, string cultureName)
    {
        // TODO: Fix this
        return LocalizeWith(source, CultureInfo.GetCultureInfo(cultureName));
    }
}