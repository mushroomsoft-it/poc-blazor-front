using MudBlazor_POC.Models;

namespace MudBlazor_POC.Services;

public class TaskService
{
    private List<Models.Task> tasks = new();
    private int nextId = 1;

    public TaskService()
    {
        // Initialize with sample data
        tasks = new List<Models.Task>
        {
            new Models.Task
            {
                Id = nextId++,
                Title = "Update documentation",
                Description = "Review and update all project documentation for clarity and accuracy",
                Status = Models.TaskStatus.Completed,
                Priority = Models.TaskPriority.Medium,
                DueDate = DateTime.Now.AddDays(-5),
                Completed = true,
                CreatedDate = DateTime.Now.AddDays(-15)
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Code review for PR #123",
                Description = "Review the authentication module changes and provide feedback",
                Status = Models.TaskStatus.InProgress,
                Priority = Models.TaskPriority.High,
                DueDate = DateTime.Now.AddDays(1),
                Completed = false,
                CreatedDate = DateTime.Now.AddDays(-3)
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Design new landing page",
                Description = "Create mockups for the new product landing page with modern design",
                Status = Models.TaskStatus.Pending,
                Priority = Models.TaskPriority.Medium,
                DueDate = DateTime.Now.AddDays(7),
                Completed = false,
                CreatedDate = DateTime.Now.AddDays(-2)
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Fix critical bug in payment",
                Description = "Investigate and resolve the payment processing timeout issue",
                Status = Models.TaskStatus.InProgress,
                Priority = Models.TaskPriority.High,
                DueDate = DateTime.Now.AddDays(2),
                Completed = false,
                CreatedDate = DateTime.Now.AddDays(-1)
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Refactor API endpoints",
                Description = "Improve REST API structure and add proper versioning",
                Status = Models.TaskStatus.Pending,
                Priority = Models.TaskPriority.Low,
                DueDate = DateTime.Now.AddDays(14),
                Completed = false,
                CreatedDate = DateTime.Now.AddDays(-1)
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Performance optimization",
                Description = "Optimize database queries and implement caching strategies",
                Status = Models.TaskStatus.InProgress,
                Priority = Models.TaskPriority.High,
                DueDate = DateTime.Now.AddDays(5),
                Completed = false,
                CreatedDate = DateTime.Now
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Write unit tests",
                Description = "Increase code coverage by adding tests for core business logic",
                Status = Models.TaskStatus.Pending,
                Priority = Models.TaskPriority.Medium,
                DueDate = DateTime.Now.AddDays(10),
                Completed = false,
                CreatedDate = DateTime.Now
            },
            new Models.Task
            {
                Id = nextId++,
                Title = "Security audit",
                Description = "Conduct comprehensive security review and fix vulnerabilities",
                Status = Models.TaskStatus.Pending,
                Priority = Models.TaskPriority.High,
                DueDate = DateTime.Now.AddDays(3),
                Completed = false,
                CreatedDate = DateTime.Now
            }
        };
    }

    public List<Models.Task> GetAll() => tasks.OrderByDescending(t => t.CreatedDate).ToList();

    public Models.Task? GetById(int id) => tasks.FirstOrDefault(t => t.Id == id);

    public void Create(Models.Task task)
    {
        task.Id = nextId++;
        task.CreatedDate = DateTime.Now;
        tasks.Add(task);
    }

    public void Update(Models.Task task)
    {
        var existing = tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing != null)
        {
            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.Priority = task.Priority;
            existing.DueDate = task.DueDate;
            existing.Completed = task.Completed;
        }
    }

    public void Delete(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
        }
    }

    public List<Models.Task> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return GetAll();

        return tasks
            .Where(t => t.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       t.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(t => t.CreatedDate)
            .ToList();
    }

    public List<Models.Task> GetByStatus(Models.TaskStatus? status)
    {
        if (status == null)
            return GetAll();

        return tasks
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedDate)
            .ToList();
    }

    public List<Models.Task> GetByPriority(Models.TaskPriority? priority)
    {
        if (priority == null)
            return GetAll();

        return tasks
            .Where(t => t.Priority == priority)
            .OrderByDescending(t => t.CreatedDate)
            .ToList();
    }

    public Dictionary<Models.TaskStatus, int> GetTasksByStatus()
    {
        return new Dictionary<Models.TaskStatus, int>
        {
            { Models.TaskStatus.Pending, tasks.Count(t => t.Status == Models.TaskStatus.Pending) },
            { Models.TaskStatus.InProgress, tasks.Count(t => t.Status == Models.TaskStatus.InProgress) },
            { Models.TaskStatus.Completed, tasks.Count(t => t.Status == Models.TaskStatus.Completed) }
        };
    }

    public Dictionary<Models.TaskPriority, int> GetTasksByPriority()
    {
        return new Dictionary<Models.TaskPriority, int>
        {
            { Models.TaskPriority.Low, tasks.Count(t => t.Priority == Models.TaskPriority.Low) },
            { Models.TaskPriority.Medium, tasks.Count(t => t.Priority == Models.TaskPriority.Medium) },
            { Models.TaskPriority.High, tasks.Count(t => t.Priority == Models.TaskPriority.High) }
        };
    }

    public int GetTotalTasks() => tasks.Count;

    public int GetCompletedTasks() => tasks.Count(t => t.Completed);

    public int GetOverdueTasks() => tasks.Count(t => !t.Completed && t.DueDate < DateTime.Now);
}
