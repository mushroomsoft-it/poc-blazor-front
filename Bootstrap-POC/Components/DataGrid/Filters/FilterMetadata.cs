using Bootstrap_POC.Components.DataGrid.Extensions;

namespace Bootstrap_POC.Components.DataGrid.Filters;

public class FilterMetadata
{
    public string PropertyName { get; set; } = string.Empty;
    public FilterType FilterType { get; set; }
    public Type PropertyType { get; set; } = typeof(object);
    public Array? EnumValues { get; set; }
    public List<string>? StringOptions { get; set; }  // For selection-based string filters
}
