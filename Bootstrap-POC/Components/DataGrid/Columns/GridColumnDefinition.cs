using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace Bootstrap_POC.Components.DataGrid.Columns;

public enum TextAlignment
{
    Left,
    Center,
    Right
}

public class GridColumnDefinition<TItem>
{
    public string PropertyName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Width { get; set; }
    public bool Sortable { get; set; } = true;
    public bool Filterable { get; set; } = true;
    public RenderFragment<TItem>? Template { get; set; }
    public string? Format { get; set; }
    public TextAlignment Alignment { get; set; } = TextAlignment.Left;
    public PropertyInfo? PropertyInfo { get; set; }
    public List<string>? FilterOptions { get; set; }  // For selection-based string filters

    public string GetDisplayTitle()
    {
        if (!string.IsNullOrEmpty(Title))
            return Title;

        return PropertyName;
    }

    public string GetAlignmentClass()
    {
        return Alignment switch
        {
            TextAlignment.Center => "text-center",
            TextAlignment.Right => "text-end",
            _ => "text-start"
        };
    }

    public object? GetValue(TItem item)
    {
        if (PropertyInfo == null)
            return null;

        return PropertyInfo.GetValue(item);
    }

    public string GetFormattedValue(TItem item)
    {
        var value = GetValue(item);

        if (value == null)
            return string.Empty;

        if (!string.IsNullOrEmpty(Format))
        {
            if (value is IFormattable formattable)
                return formattable.ToString(Format, null);
        }

        return value.ToString() ?? string.Empty;
    }
}
