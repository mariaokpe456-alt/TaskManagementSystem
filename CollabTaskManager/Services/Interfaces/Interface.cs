using CollabTaskManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollabTaskManager.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId);
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem> UpdateTaskAsync(TaskItem task);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<bool> AssignTaskAsync(Guid taskId, string userId);
        Task<bool> UpdateTaskStatusAsync(Guid taskId, string status);
        Task<bool> UpdateTaskPriorityAsync(Guid taskId, string priority);
    }
}

