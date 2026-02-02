using Microsoft.AspNetCore.Components;
using Bootstrap_POC.Components.DataGrid.Columns;
using Bootstrap_POC.Components.DataGrid.Filters;
using Bootstrap_POC.Components.DataGrid.Extensions;
using System.Reflection;

namespace Bootstrap_POC.Components.DataGrid;

public partial class DataGrid<TItem> : ComponentBase
{
    [Parameter] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();
    [Parameter] public int TotalCount { get; set; }

    [Parameter] public int CurrentPage { get; set; } = 1;
    [Parameter] public int PageSize { get; set; } = 10;
    [Parameter] public EventCallback<int> OnPageChanged { get; set; }

    [Parameter] public string? SortColumn { get; set; }
    [Parameter] public bool SortAscending { get; set; } = true;
    [Parameter] public EventCallback<(string Column, bool Ascending)> OnSortChanged { get; set; }

    [Parameter] public EventCallback<FilterChangeEventArgs> OnFilterChanged { get; set; }

    [Parameter] public RenderFragment? Columns { get; set; }
    [Parameter] public List<GridColumnDefinition<TItem>>? ColumnDefinitions { get; set; }

    [Parameter] public RenderFragment<TItem>? RowActions { get; set; }
    [Parameter] public RenderFragment? EmptyContent { get; set; }
    [Parameter] public bool FixedLayout { get; set; } = true;
    [Parameter] public bool ShowFilters { get; set; } = true;

    private List<GridColumnDefinition<TItem>> _resolvedColumns = new();
    private List<GridColumn<TItem>> _declarativeColumns = new();
    private Dictionary<string, FilterState> _activeFilters = new();
    private Dictionary<string, FilterMetadata> _filterMetadata = new();
    private bool _columnsInitialized = false;

    protected override void OnParametersSet()
    {
        // Only resolve if columns are already initialized or if we're using programmatic definitions
        if (_columnsInitialized || ColumnDefinitions != null)
        {
            ResolveColumns();
            InitializeFilterMetadata();
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender && !_columnsInitialized)
        {
            _columnsInitialized = true;
            ResolveColumns();
            InitializeFilterMetadata();
            StateHasChanged();
        }
    }

    internal void AddColumn(GridColumn<TItem> column)
    {
        if (!_declarativeColumns.Contains(column))
        {
            _declarativeColumns.Add(column);
        }
    }

    private void ResolveColumns()
    {
        _resolvedColumns.Clear();

        // Priority 1: Programmatic ColumnDefinitions
        if (ColumnDefinitions != null && ColumnDefinitions.Any())
        {
            _resolvedColumns.AddRange(ColumnDefinitions);
        }
        // Priority 2: Declarative Columns
        else if (_declarativeColumns.Any())
        {
            _resolvedColumns.AddRange(_declarativeColumns.Select(c => c.ToColumnDefinition()));
        }
        // Priority 3: Auto-detect from TItem properties
        else
        {
            var properties = typeof(TItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                _resolvedColumns.Add(new GridColumnDefinition<TItem>
                {
                    PropertyName = prop.Name,
                    Title = prop.Name,
                    PropertyInfo = prop
                });
            }
        }

        // Set PropertyInfo for all columns
        foreach (var column in _resolvedColumns)
        {
            if (column.PropertyInfo == null)
            {
                column.PropertyInfo = typeof(TItem).GetProperty(column.PropertyName);
            }
        }
    }

    private void InitializeFilterMetadata()
    {
        _filterMetadata.Clear();

        if (!ShowFilters) return;

        foreach (var column in _resolvedColumns.Where(c => c.Filterable && c.PropertyInfo != null))
        {
            var propertyType = column.PropertyInfo!.PropertyType;
            var filterType = propertyType.GetFilterType();

            var metadata = new FilterMetadata
            {
                PropertyName = column.PropertyName,
                FilterType = filterType,
                PropertyType = propertyType
            };

            if (filterType == FilterType.Enum)
            {
                var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                metadata.EnumValues = Enum.GetValues(underlyingType);
            }

            // If FilterOptions are provided for string columns, treat as selection
            if (filterType == FilterType.String && column.FilterOptions != null && column.FilterOptions.Any())
            {
                metadata.StringOptions = column.FilterOptions;
            }

            _filterMetadata[column.PropertyName] = metadata;
        }
    }

    private async Task HandleSortClick(string propertyName)
    {
        var column = _resolvedColumns.FirstOrDefault(c => c.PropertyName == propertyName);
        if (column == null || !column.Sortable)
            return;

        bool ascending = true;
        if (SortColumn == propertyName)
        {
            ascending = !SortAscending;
        }

        await OnSortChanged.InvokeAsync((propertyName, ascending));
    }

    private string GetSortIconClass(string propertyName)
    {
        if (SortColumn != propertyName)
            return "fas fa-sort text-muted";

        return SortAscending ? "fas fa-sort-up" : "fas fa-sort-down";
    }

    private async Task HandleFilterChanged(string propertyName, FilterState? filterState)
    {
        if (filterState == null || !filterState.IsActive())
        {
            _activeFilters.Remove(propertyName);
        }
        else
        {
            _activeFilters[propertyName] = filterState;
        }

        var args = new FilterChangeEventArgs
        {
            PropertyName = propertyName,
            FilterState = filterState,
            AllFilters = new Dictionary<string, FilterState>(_activeFilters)
        };

        await OnFilterChanged.InvokeAsync(args);
    }

    private bool IsFilterActive(string propertyName)
    {
        return _activeFilters.ContainsKey(propertyName) && _activeFilters[propertyName].IsActive();
    }

    private async Task HandlePageChangedInternal(int page)
    {
        await OnPageChanged.InvokeAsync(page);
    }
}
