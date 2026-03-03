using Microsoft.JSInterop;

public class TeeChartService : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;

    public TeeChartService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        _module = await _js.InvokeAsync<IJSObjectReference>(
            "import", "./js/teechart/chartFactory.js");
    }

    public async Task CreateChartAsync(string type, string canvasId,
        List<double> data, object? options = null)
    {
        if (_module is null)
            throw new InvalidOperationException("JS Module no inicializado");

        await _module.InvokeVoidAsync(
            "createChart",
            type,
            canvasId,
            data,
            options);
    }

    public async Task UpdateChartAsync(
    string type,
    string canvasId,
    List<double> data,
    object? options = null)
    {
        if (_module is null)
            return;

        await _module.InvokeVoidAsync(
            "updateChart",
            canvasId,
            data,
            type,
            options);
    }
    public async ValueTask DisposeAsync()
    {
        if (_module != null)
            await _module.DisposeAsync();
    }
}