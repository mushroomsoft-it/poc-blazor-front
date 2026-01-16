namespace FluentUI_POC.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.Pending;
    public Models.TaskPriority Priority { get; set; } = Models.TaskPriority.Medium;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
    public bool Completed { get; set; }
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}
