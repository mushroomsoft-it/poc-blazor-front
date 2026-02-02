using Bootstrap_POC.Components.DataGrid.Extensions;

namespace Bootstrap_POC.Components.DataGrid.FilterPanel;

/// <summary>
/// Defines a column that can be filtered in the FilterPanel
/// </summary>
public class FilterableColumn
{
    public string PropertyName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public FilterType FilterType { get; set; }
    public Type PropertyType { get; set; } = typeof(string);
    public Array? EnumValues { get; set; }
    public List<string>? SelectionOptions { get; set; }

    /// <summary>
    /// Gets the available operators for this column based on its filter type
    /// </summary>
    public List<FilterOperatorOption> GetAvailableOperators()
    {
        return FilterType switch
        {
            FilterType.String => new List<FilterOperatorOption>
            {
                new("Contains", "contains"),
                new("Equals", "equals"),
                new("Starts with", "startsWith"),
                new("Ends with", "endsWith")
            },
            FilterType.Numeric => new List<FilterOperatorOption>
            {
                new("Equals", "equals"),
                new("Greater than", "greaterThan"),
                new("Less than", "lessThan"),
                new("Between", "between")
            },
            FilterType.Date => new List<FilterOperatorOption>
            {
                new("Equals", "equals"),
                new("After", "after"),
                new("Before", "before"),
                new("Between", "between")
            },
            FilterType.Boolean => new List<FilterOperatorOption>
            {
                new("Is true", "true"),
                new("Is false", "false")
            },
            FilterType.Enum => new List<FilterOperatorOption>
            {
                new("Equals", "equals")
            },
            _ => new List<FilterOperatorOption>()
        };
    }
}

public class FilterOperatorOption
{
    public string Label { get; set; }
    public string Value { get; set; }

    public FilterOperatorOption(string label, string value)
    {
        Label = label;
        Value = value;
    }
}
