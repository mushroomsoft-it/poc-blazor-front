namespace Bootstrap_POC.Components.DataGrid.Extensions;

public enum FilterType
{
    String,
    Numeric,
    Date,
    Boolean,
    Enum,
    None
}

public static class TypeExtensions
{
    public static FilterType GetFilterType(this Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        if (underlyingType == typeof(string))
            return FilterType.String;

        if (underlyingType == typeof(int) ||
            underlyingType == typeof(long) ||
            underlyingType == typeof(decimal) ||
            underlyingType == typeof(double) ||
            underlyingType == typeof(float) ||
            underlyingType == typeof(short) ||
            underlyingType == typeof(byte))
            return FilterType.Numeric;

        if (underlyingType == typeof(DateTime) ||
            underlyingType == typeof(DateTimeOffset) ||
            underlyingType == typeof(DateOnly))
            return FilterType.Date;

        if (underlyingType == typeof(bool))
            return FilterType.Boolean;

        if (underlyingType.IsEnum)
            return FilterType.Enum;

        return FilterType.None;
    }
}
