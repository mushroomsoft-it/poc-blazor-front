using Microsoft.AspNetCore.Components;
using Bootstrap_POC.Components.DataGrid.Filters;
using Bootstrap_POC.Components.DataGrid.Extensions;

namespace Bootstrap_POC.Components.DataGrid.FilterPanel;

public partial class FilterPanel : ComponentBase
{
    /// <summary>
    /// List of columns that can be filtered
    /// </summary>
    [Parameter]
    public List<FilterableColumn> Columns { get; set; } = new();

    /// <summary>
    /// Current active filters (two-way binding)
    /// </summary>
    [Parameter]
    public Dictionary<string, FilterState> ActiveFilters { get; set; } = new();

    [Parameter]
    public EventCallback<Dictionary<string, FilterState>> ActiveFiltersChanged { get; set; }

    /// <summary>
    /// Event fired when filters change
    /// </summary>
    [Parameter]
    public EventCallback<FilterChangeEventArgs> OnFilterChanged { get; set; }

    /// <summary>
    /// Whether to show the save view functionality
    /// </summary>
    [Parameter]
    public bool ShowSaveView { get; set; } = true;

    /// <summary>
    /// Saved filter views
    /// </summary>
    [Parameter]
    public List<FilterView> SavedViews { get; set; } = new();

    [Parameter]
    public EventCallback<List<FilterView>> SavedViewsChanged { get; set; }

    /// <summary>
    /// Event fired when a view is saved
    /// </summary>
    [Parameter]
    public EventCallback<FilterView> OnViewSaved { get; set; }

    /// <summary>
    /// Event fired when a view is loaded
    /// </summary>
    [Parameter]
    public EventCallback<FilterView> OnViewLoaded { get; set; }

    // Internal state
    private Dictionary<string, FilterState> _activeFilters = new();
    private List<FilterView> _savedViews = new();
    private bool _isPanelOpen = false;
    private bool _isConfiguringFilter = false;
    private bool _isSaveViewDialogOpen = false;
    private FilterableColumn? _selectedColumn = null;
    private string _selectedOperator = "";
    private string _filterValue = "";
    private string _filterValue2 = "";
    private DateTime? _filterDateValue = null;
    private DateTime? _filterDateValue2 = null;
    private string _newViewName = "";
    private readonly string _buttonId = $"filter-btn-{Guid.NewGuid():N}";
    private readonly string _dropdownId = $"filter-dropdown-{Guid.NewGuid():N}";

    public int ActiveFilterCount => _activeFilters.Count;

    protected override void OnParametersSet()
    {
        _activeFilters = new Dictionary<string, FilterState>(ActiveFilters);
        _savedViews = new List<FilterView>(SavedViews);
    }

    private void TogglePanel()
    {
        _isPanelOpen = !_isPanelOpen;
        if (!_isPanelOpen)
        {
            ResetConfigState();
        }
    }

    private void ClosePanel()
    {
        _isPanelOpen = false;
        ResetConfigState();
    }

    private void ResetConfigState()
    {
        _isConfiguringFilter = false;
        _selectedColumn = null;
        _selectedOperator = "";
        _filterValue = "";
        _filterValue2 = "";
        _filterDateValue = null;
        _filterDateValue2 = null;
    }

    private void SelectColumnToFilter(FilterableColumn column)
    {
        _selectedColumn = column;
        _isConfiguringFilter = true;

        // Set default operator
        var operators = column.GetAvailableOperators();
        _selectedOperator = operators.FirstOrDefault()?.Value ?? "";

        // Clear values
        _filterValue = "";
        _filterValue2 = "";
        _filterDateValue = null;
        _filterDateValue2 = null;
    }

    private void EditFilter(FilterableColumn column)
    {
        _selectedColumn = column;
        _isConfiguringFilter = true;

        if (_activeFilters.TryGetValue(column.PropertyName, out var existingFilter))
        {
            _selectedOperator = MapOperatorToString(existingFilter.Operator, column.FilterType);

            if (column.FilterType == FilterType.Date)
            {
                _filterDateValue = existingFilter.Value as DateTime?;
                _filterDateValue2 = existingFilter.Value2 as DateTime?;
            }
            else
            {
                _filterValue = existingFilter.Value?.ToString() ?? "";
                _filterValue2 = existingFilter.Value2?.ToString() ?? "";
            }
        }
    }

    private void CancelFilterConfig()
    {
        ResetConfigState();
    }

    private async Task ApplyFilter()
    {
        if (_selectedColumn == null) return;

        var filterState = new FilterState
        {
            PropertyName = _selectedColumn.PropertyName,
            Operator = MapStringToOperator(_selectedOperator, _selectedColumn.FilterType)
        };

        // Set value based on filter type
        switch (_selectedColumn.FilterType)
        {
            case FilterType.String:
                filterState.Value = _filterValue;
                break;

            case FilterType.Numeric:
                if (decimal.TryParse(_filterValue, out var numValue))
                    filterState.Value = numValue;
                if (_selectedOperator == "between" && decimal.TryParse(_filterValue2, out var numValue2))
                    filterState.Value2 = numValue2;
                break;

            case FilterType.Date:
                filterState.Value = _filterDateValue;
                if (_selectedOperator == "between")
                    filterState.Value2 = _filterDateValue2;
                break;

            case FilterType.Boolean:
                filterState.Value = _selectedOperator == "true";
                filterState.Operator = FilterOperator.Equals;
                break;

            case FilterType.Enum:
                if (!string.IsNullOrEmpty(_filterValue) && _selectedColumn.EnumValues != null)
                {
                    var enumType = _selectedColumn.PropertyType;
                    var underlyingType = Nullable.GetUnderlyingType(enumType) ?? enumType;
                    if (Enum.TryParse(underlyingType, _filterValue, out var enumValue))
                        filterState.Value = enumValue;
                }
                break;
        }

        if (filterState.IsActive())
        {
            _activeFilters[_selectedColumn.PropertyName] = filterState;
            await NotifyFilterChanged(_selectedColumn.PropertyName, filterState);
        }

        ResetConfigState();
    }

    private async Task RemoveFilter(string propertyName)
    {
        if (_activeFilters.Remove(propertyName))
        {
            await NotifyFilterChanged(propertyName, null);
        }
    }

    private async Task ResetAllFilters()
    {
        _activeFilters.Clear();
        await NotifyFilterChanged(string.Empty, null);
    }

    private async Task NotifyFilterChanged(string propertyName, FilterState? filterState)
    {
        await ActiveFiltersChanged.InvokeAsync(new Dictionary<string, FilterState>(_activeFilters));

        var args = new FilterChangeEventArgs
        {
            PropertyName = propertyName,
            FilterState = filterState,
            AllFilters = new Dictionary<string, FilterState>(_activeFilters)
        };

        await OnFilterChanged.InvokeAsync(args);
    }

    private void OpenSaveViewDialog()
    {
        _newViewName = "";
        _isSaveViewDialogOpen = true;
    }

    private void CloseSaveViewDialog()
    {
        _isSaveViewDialogOpen = false;
        _newViewName = "";
    }

    private async Task SaveView()
    {
        if (string.IsNullOrWhiteSpace(_newViewName)) return;

        var view = new FilterView
        {
            Name = _newViewName,
            Filters = _activeFilters.ToDictionary(
                kvp => kvp.Key,
                kvp => new FilterViewItem
                {
                    PropertyName = kvp.Value.PropertyName,
                    Operator = kvp.Value.Operator.ToString(),
                    Value = kvp.Value.Value?.ToString(),
                    Value2 = kvp.Value.Value2?.ToString(),
                    ValueType = kvp.Value.Value?.GetType().FullName
                }
            )
        };

        _savedViews.Add(view);
        await SavedViewsChanged.InvokeAsync(new List<FilterView>(_savedViews));
        await OnViewSaved.InvokeAsync(view);

        CloseSaveViewDialog();
    }

    private async Task LoadSelectedView(ChangeEventArgs e)
    {
        var viewId = e.Value?.ToString();
        if (string.IsNullOrEmpty(viewId)) return;

        var view = _savedViews.FirstOrDefault(v => v.Id == viewId);
        if (view == null) return;

        _activeFilters.Clear();

        foreach (var filterItem in view.Filters)
        {
            var column = Columns.FirstOrDefault(c => c.PropertyName == filterItem.Key);
            if (column == null) continue;

            var filterState = new FilterState
            {
                PropertyName = filterItem.Value.PropertyName,
                Operator = Enum.TryParse<FilterOperator>(filterItem.Value.Operator, out var op)
                    ? op : FilterOperator.Equals
            };

            // Parse value based on column type
            filterState.Value = ParseFilterValue(filterItem.Value.Value, column);
            filterState.Value2 = ParseFilterValue(filterItem.Value.Value2, column);

            if (filterState.IsActive())
            {
                _activeFilters[filterItem.Key] = filterState;
            }
        }

        await NotifyFilterChanged(string.Empty, null);
        await OnViewLoaded.InvokeAsync(view);
    }

    private object? ParseFilterValue(string? value, FilterableColumn column)
    {
        if (string.IsNullOrEmpty(value)) return null;

        return column.FilterType switch
        {
            FilterType.Numeric => decimal.TryParse(value, out var num) ? num : null,
            FilterType.Date => DateTime.TryParse(value, out var date) ? date : null,
            FilterType.Boolean => bool.TryParse(value, out var b) ? b : null,
            FilterType.Enum => ParseEnumValue(value, column),
            _ => value
        };
    }

    private object? ParseEnumValue(string value, FilterableColumn column)
    {
        var underlyingType = Nullable.GetUnderlyingType(column.PropertyType) ?? column.PropertyType;
        return Enum.TryParse(underlyingType, value, out var result) ? result : null;
    }

    private FilterOperator MapStringToOperator(string operatorString, FilterType filterType)
    {
        return operatorString switch
        {
            "contains" => FilterOperator.Contains,
            "equals" => FilterOperator.Equals,
            "startsWith" => FilterOperator.StartsWith,
            "endsWith" => FilterOperator.EndsWith,
            "greaterThan" => FilterOperator.GreaterThan,
            "lessThan" => FilterOperator.LessThan,
            "between" => FilterOperator.Between,
            "after" => FilterOperator.After,
            "before" => FilterOperator.Before,
            "true" => FilterOperator.Equals,
            "false" => FilterOperator.Equals,
            _ => FilterOperator.Equals
        };
    }

    private string MapOperatorToString(FilterOperator op, FilterType filterType)
    {
        return op switch
        {
            FilterOperator.Contains => "contains",
            FilterOperator.Equals => filterType == FilterType.Boolean ? "true" : "equals",
            FilterOperator.StartsWith => "startsWith",
            FilterOperator.EndsWith => "endsWith",
            FilterOperator.GreaterThan => "greaterThan",
            FilterOperator.LessThan => "lessThan",
            FilterOperator.Between => "between",
            FilterOperator.After => "after",
            FilterOperator.Before => "before",
            _ => "equals"
        };
    }

    private string GetFilterTypeIcon(FilterType filterType)
    {
        return filterType switch
        {
            FilterType.String => "fas fa-font",
            FilterType.Numeric => "fas fa-hashtag",
            FilterType.Date => "fas fa-calendar",
            FilterType.Boolean => "fas fa-toggle-on",
            FilterType.Enum => "fas fa-list",
            _ => "fas fa-filter"
        };
    }

    private string GetFilterDescription(FilterState filter, FilterableColumn column)
    {
        var opDescription = filter.Operator switch
        {
            FilterOperator.Contains => "contains",
            FilterOperator.Equals => "is",
            FilterOperator.StartsWith => "starts with",
            FilterOperator.EndsWith => "ends with",
            FilterOperator.GreaterThan => ">",
            FilterOperator.LessThan => "<",
            FilterOperator.Between => "between",
            FilterOperator.After => "after",
            FilterOperator.Before => "before",
            _ => "is"
        };

        var valueStr = FormatFilterValue(filter.Value, column);

        if (filter.Operator == FilterOperator.Between && filter.Value2 != null)
        {
            var value2Str = FormatFilterValue(filter.Value2, column);
            return $"{opDescription} {valueStr} and {value2Str}";
        }

        return $"{opDescription} {valueStr}";
    }

    private string FormatFilterValue(object? value, FilterableColumn column)
    {
        if (value == null) return "";

        return column.FilterType switch
        {
            FilterType.Date when value is DateTime date => date.ToString("dd/MM/yyyy"),
            FilterType.Boolean when value is bool b => b ? "Yes" : "No",
            _ => value.ToString() ?? ""
        };
    }
}
