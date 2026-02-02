namespace Bootstrap_POC.Components.DataGrid.Filters;

public class FilterChangeEventArgs
{
    public string PropertyName { get; set; } = string.Empty;
    public FilterState? FilterState { get; set; }
    public Dictionary<string, FilterState> AllFilters { get; set; } = new();
}
