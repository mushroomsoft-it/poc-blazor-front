using FluentUI_POC.Models;

namespace FluentUI_POC.Services;

public class TaskService
{
    private List<Models.Task> _tasks = new();
    private int _nextId = 1;

    public TaskService()
    {
        // Sample data
        _tasks = new List<Models.Task>
        {
            new Models.Task { Id = _nextId++, Title = "Implement authentication", Description = "Add JWT login", Status = Models.TaskStatus.InProgress, Priority = Models.TaskPriority.High, DueDate = DateTime.Now.AddDays(3) },
            new Models.Task { Id = _nextId++, Title = "Design homepage", Description = "Create dashboard mockups", Status = Models.TaskStatus.Completed, Priority = Models.TaskPriority.Medium, DueDate = DateTime.Now.AddDays(-2), Completed = true },
            new Models.Task { Id = _nextId++, Title = "Setup CI/CD", Description = "Configure Azure DevOps pipeline", Status = Models.TaskStatus.Pending, Priority = Models.TaskPriority.High, DueDate = DateTime.Now.AddDays(5) },
            new Models.Task { Id = _nextId++, Title = "Write unit tests", Description = "Minimum 80% code coverage", Status = Models.TaskStatus.InProgress, Priority = Models.TaskPriority.Medium, DueDate = DateTime.Now.AddDays(7) },
            new Models.Task { Id = _nextId++, Title = "Optimize SQL queries", Description = "Improve query performance", Status = Models.TaskStatus.Pending, Priority = Models.TaskPriority.Low, DueDate = DateTime.Now.AddDays(14) },
            new Models.Task { Id = _nextId++, Title = "Update documentation", Description = "Document new APIs", Status = Models.TaskStatus.Pending, Priority = Models.TaskPriority.Low, DueDate = DateTime.Now.AddDays(10) },
            new Models.Task { Id = _nextId++, Title = "Review team code", Description = "Code review pending PRs", Status = Models.TaskStatus.InProgress, Priority = Models.TaskPriority.Medium, DueDate = DateTime.Now.AddDays(1) },
            new Models.Task { Id = _nextId++, Title = "Migrate to .NET 10", Description = "Update project to latest version", Status = Models.TaskStatus.Completed, Priority = Models.TaskPriority.High, DueDate = DateTime.Now.AddDays(-5), Completed = true }
        };
    }

    public List<Models.Task> GetAll()
    {
        return _tasks.OrderBy(t => t.Completed).ThenByDescending(t => t.Priority).ToList();
    }

    public Models.Task? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public Models.Task Create(Models.Task task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return task;
    }

    public bool Update(Models.Task task)
    {
        var existing = GetById(task.Id);
        if (existing == null) return false;

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.Status = task.Status;
        existing.Priority = task.Priority;
        existing.DueDate = task.DueDate;
        existing.Completed = task.Completed;
        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task == null) return false;

        _tasks.Remove(task);
        return true;
    }

    public bool ToggleCompleted(int id)
    {
        var task = GetById(id);
        if (task == null) return false;

        task.Completed = !task.Completed;
        if (task.Completed)
            task.Status = Models.TaskStatus.Completed;
        return true;
    }

    public List<Models.Task> GetByStatus(Models.TaskStatus? status)
    {
        if (status == null)
            return GetAll();

        return _tasks.Where(t => t.Status == status).OrderByDescending(t => t.Priority).ToList();
    }

    public List<Models.Task> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return GetAll();

        searchTerm = searchTerm.ToLower();
        return _tasks
            .Where(t => t.Title.ToLower().Contains(searchTerm) ||
                       t.Description.ToLower().Contains(searchTerm))
            .OrderBy(t => t.Completed)
            .ThenByDescending(t => t.Priority)
            .ToList();
    }
}
