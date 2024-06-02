using BuildingBlocks.Domain.Exceptions;
using System.Reflection;

namespace BuildingBlocks.Domain.Enums;

public abstract record Enumeration<TEnum> where TEnum : Enumeration<TEnum>
{
    private static Dictionary<int, Enumeration<TEnum>> _enumerations = new Dictionary<int, Enumeration<TEnum>>();
    protected Enumeration(int key, string value)
    {
        Key = key;
        Value = value;

        _enumerations.Add(key, this);
    }

    public int Key { get; protected init; }
    public string Value { get; protected init; } = string.Empty;

    public static Enumeration<TEnum> FromKey(int key)
    {
        if (!_enumerations.Any())
            _enumerations = GetEnurametions();

        _enumerations.TryGetValue(key, out Enumeration<TEnum> value);

        return value;
    }
    public static Enumeration<TEnum> FromValue(string value)
    {
        if (!_enumerations.Any())
            _enumerations = GetEnurametions();

        var enumarationValue = _enumerations.Select(c => c.Value).FirstOrDefault(c => c.Value == value);

        if (enumarationValue is null)
            throw new UnsupportedEnumException(value);

        return enumarationValue;
    }

    public static explicit operator Enumeration<TEnum>(string value)
    {
        return FromValue(value);
    }

    public static implicit operator string(Enumeration<TEnum> userStatus)
    {
        return userStatus.Value;
    }

    private static Dictionary<int, Enumeration<TEnum>> GetEnurametions()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(field => enumerationType.IsAssignableFrom(field.FieldType))
            .Select(field => (Enumeration<TEnum>)field.GetValue(default));

        return fieldsForType.ToDictionary(x => x.Key, y => y);
    }

    public sealed override string ToString()
    {
        return Value;
    }
}