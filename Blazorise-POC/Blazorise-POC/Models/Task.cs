using System.ComponentModel.DataAnnotations;

namespace Blazorise_POC.Models;

public class Task
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = string.Empty;

    public Blazorise_POC.Models.TaskStatus Status { get; set; } = Blazorise_POC.Models.TaskStatus.Pending;
    public Blazorise_POC.Models.TaskPriority Priority { get; set; } = Blazorise_POC.Models.TaskPriority.Medium;
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
