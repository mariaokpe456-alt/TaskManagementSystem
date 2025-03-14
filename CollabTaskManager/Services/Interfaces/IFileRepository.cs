using CollabTaskManager.Models;

namespace CollabTaskManager.Services.Interfaces
{
    public interface IFileRepository
    {
        Task<TaskFile> UploadFileAsync(TaskFile file);
        Task<IEnumerable<TaskFile>> GetFilesByTaskIdAsync(Guid taskId);
    }
}
