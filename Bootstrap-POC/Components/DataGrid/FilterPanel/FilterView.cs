namespace Bootstrap_POC.Components.DataGrid.FilterPanel;

/// <summary>
/// Represents a saved filter configuration (view)
/// </summary>
public class FilterView
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, FilterViewItem> Filters { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

/// <summary>
/// Serializable filter item for saving in views
/// </summary>
public class FilterViewItem
{
    public string PropertyName { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public string? Value { get; set; }
    public string? Value2 { get; set; }
    public string? ValueType { get; set; }
}
