namespace Bootstrap_POC.Components.DataGrid.Filters;

public enum FilterOperator
{
    Equals,
    NotEquals,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    Between,
    Contains,
    StartsWith,
    EndsWith,
    After,
    Before
}

public class FilterState
{
    public string PropertyName { get; set; } = string.Empty;
    public FilterOperator Operator { get; set; } = FilterOperator.Equals;
    public object? Value { get; set; }
    public object? Value2 { get; set; }

    public bool IsActive()
    {
        if (Operator == FilterOperator.Between)
            return Value != null && Value2 != null;

        return Value != null;
    }
}
