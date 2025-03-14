// ✅ Ensure your TaskItem model has timestamps
public class TaskItem
{
    public Guid Id { get; set; }
    //public string Title { get; set; }
    public string   Title { get; set; } = string.Empty;
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string AssignedTo { get; set; }
    public Guid ProjectId { get; set; }

    // ✅ Timestamps for tracking task creation and updates
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}