using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harmony.EntityFrameworkCore.SourceGenerators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LocalizableColumnAttribute : NotMappedAttribute
    {
    }
}