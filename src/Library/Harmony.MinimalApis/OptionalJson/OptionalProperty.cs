namespace Harmony.MinimalApis.OptionalJson;

using System;
using System.Collections.Generic;

public readonly struct OptionalProperty<T> : IEquatable<OptionalProperty<T>>
{
    public OptionalProperty(T? value, bool hasValue)
    {
        Value = value;
        HasValue = hasValue;
    }

    /// <summary>
    /// Represents an unset optional property (no value provided).
    /// </summary>
    public static OptionalProperty<T> None => new(default, false);
    
    public bool HasValue { get; }
    public T? Value { get; }
    
    public static implicit operator OptionalProperty<T>(T? value) => new(value, true);
    public static bool operator ==(OptionalProperty<T> left, OptionalProperty<T> right) =>
        left.Equals(right);

    public static bool operator !=(OptionalProperty<T> left, OptionalProperty<T> right) =>
        !(left == right);

    public OptionalProperty<T> ToOptionalProperty(T? value) => new(value, true);

    public override bool Equals(object? obj) =>
        obj is OptionalProperty<T> other && Equals(other);

    public bool Equals(OptionalProperty<T> other)
    {
        if (HasValue != other.HasValue)
        {
            return false;
        }

        // both are "not set"
        if (!HasValue) 
        {
            return true;
        }

        return EqualityComparer<T?>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode()
    {
        if (!HasValue)
        {
            return 0;
        }
        
        var valueHash = Value is null ? 0 : EqualityComparer<T>.Default.GetHashCode(Value);
        return HashCode.Combine(true, valueHash);
    }

    public override string ToString() =>
        HasValue ? Value?.ToString() ?? "null" : "<unset>";
}
