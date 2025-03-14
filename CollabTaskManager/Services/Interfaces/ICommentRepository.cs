using CollabTaskManager.Models;

namespace CollabTaskManager.Services.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(Guid taskId);
        Task<Comment> AddCommentAsync(Comment comment);
    }
}
