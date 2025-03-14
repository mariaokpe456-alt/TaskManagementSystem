namespace CollabTaskManager.Models.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
    }
}
