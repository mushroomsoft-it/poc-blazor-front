using Radzen_POC.Models;

namespace Radzen_POC.Services;

public class StatisticsService
{
    private readonly ProductService _productService;
    private readonly TaskService _taskService;

    public StatisticsService(ProductService productService, TaskService taskService)
    {
        _productService = productService;
        _taskService = taskService;
    }

    public int GetTotalProducts()
    {
        return _productService.GetAll().Count;
    }

    public decimal GetTotalInventoryValue()
    {
        return _productService.GetAll().Sum(p => p.Price * p.Stock);
    }

    public int GetLowStockProducts()
    {
        return _productService.GetAll().Count(p => p.Stock < 20);
    }

    public int GetTotalTasks()
    {
        return _taskService.GetAll().Count;
    }

    public int GetPendingTasks()
    {
        return _taskService.GetAll().Count(t => !t.Completed);
    }

    public int GetCompletedTasks()
    {
        return _taskService.GetAll().Count(t => t.Completed);
    }

    public int GetOverdueTasks()
    {
        return _taskService.GetAll().Count(t => !t.Completed && t.DueDate < DateTime.Now);
    }

    public Dictionary<string, int> GetProductsByCategory()
    {
        return _productService.GetAll()
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public Dictionary<Models.TaskStatus, int> GetTasksByStatus()
    {
        return _taskService.GetAll()
            .GroupBy(t => t.Status)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public Dictionary<Models.TaskPriority, int> GetTasksByPriority()
    {
        return _taskService.GetAll()
            .Where(t => !t.Completed)
            .GroupBy(t => t.Priority)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
