namespace CollabTaskManager.Models.DTOs
{
    public class CommentDto
    {
        public Guid TaskId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
