namespace MudBlazor_POC.Services;

public class StatisticsService
{
    private readonly ProductService _productService;
    private readonly TaskService _taskService;

    public StatisticsService(ProductService productService, TaskService taskService)
    {
        _productService = productService;
        _taskService = taskService;
    }

    public int GetTotalProducts() => _productService.GetTotalProducts();

    public decimal GetTotalValue() => _productService.GetTotalValue();

    public int GetLowStockCount() => _productService.GetLowStockCount();

    public int GetTotalTasks() => _taskService.GetTotalTasks();

    public int GetCompletedTasks() => _taskService.GetCompletedTasks();

    public int GetPendingTasks() => GetTotalTasks() - GetCompletedTasks();

    public int GetOverdueTasks() => _taskService.GetOverdueTasks();

    public double GetTaskCompletionRate()
    {
        var total = GetTotalTasks();
        if (total == 0) return 0;
        return (double)GetCompletedTasks() / total * 100;
    }
}
