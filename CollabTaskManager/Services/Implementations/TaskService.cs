//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class TaskService : ITaskService
//    {
//        private readonly ITaskRepository _taskRepository;
//        private readonly ILogger<TaskService> _logger;

//        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
//        {
//            _taskRepository = taskRepository;
//            _logger = logger;
//        }

//        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId)
//        {
//            _logger.LogInformation("Retrieving tasks for project ID: {ProjectId}", projectId);
//            return await _taskRepository.GetTasksByProjectIdAsync(projectId);
//        }

//        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
//        {
//            _logger.LogInformation("Fetching task with ID: {TaskId}", id);
//            return await _taskRepository.GetTaskByIdAsync(id);
//        }

//        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
//        {
//            _logger.LogInformation("Creating a new task: {TaskTitle}", task.Title);
//            return await _taskRepository.CreateTaskAsync(task);
//        }

//        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
//        {
//            _logger.LogInformation("Updating task with ID: {TaskId}", task.Id);
//            return await _taskRepository.UpdateTaskAsync(task);
//        }

//        public async Task<bool> DeleteTaskAsync(Guid id)
//        {
//            _logger.LogInformation("Deleting task with ID: {TaskId}", id);
//            return await _taskRepository.DeleteTaskAsync(id);
//        }

//        public async Task<bool> AssignTaskAsync(Guid taskId, string userId)
//        {
//            _logger.LogInformation("Assigning task {TaskId} to user {UserId}", taskId, userId);
//            return await _taskRepository.AssignTaskAsync(taskId, userId);
//        }

//        public async Task<bool> UpdateTaskStatusAsync(Guid taskId, string status)
//        {
//            _logger.LogInformation("Updating status of task {TaskId} to {Status}", taskId, status);
//            return await _taskRepository.UpdateTaskStatusAsync(taskId, status);
//        }

//        public async Task<bool> UpdateTaskPriorityAsync(Guid taskId, string priority)
//        {
//            _logger.LogInformation("Updating priority of task {TaskId} to {Priority}", taskId, priority);
//            return await _taskRepository.UpdateTaskPriorityAsync(taskId, priority);
//        }
//    }
//}


using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollabTaskManager.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId)
        {
            _logger.LogInformation("Retrieving tasks for project ID: {ProjectId}", projectId);
            return await _taskRepository.GetTasksByProjectIdAsync(projectId);
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching task with ID: {TaskId}", id);
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _logger.LogInformation("Creating a new task: {TaskTitle}", task.Title);

            // ✅ Set CreatedAt and UpdatedAt timestamps for new task
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            return await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            _logger.LogInformation("Updating task with ID: {TaskId}", task.Id);

            // ✅ Update the timestamp when task is modified
            task.UpdatedAt = DateTime.UtcNow;

            return await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            _logger.LogInformation("Deleting task with ID: {TaskId}", id);
            return await _taskRepository.DeleteTaskAsync(id);
        }

        public async Task<bool> AssignTaskAsync(Guid taskId, string userId)
        {
            _logger.LogInformation("Assigning task {TaskId} to user {UserId}", taskId, userId);
            return await _taskRepository.AssignTaskAsync(taskId, userId);
        }

        public async Task<bool> UpdateTaskStatusAsync(Guid taskId, string status)
        {
            _logger.LogInformation("Updating status of task {TaskId} to {Status}", taskId, status);

            // ✅ Fetch the task and update timestamp
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return false;

            task.Status = status;
            task.UpdatedAt = DateTime.UtcNow; // ✅ Update timestamp

            await _taskRepository.UpdateTaskAsync(task);
            return true;
        }

        public async Task<bool> UpdateTaskPriorityAsync(Guid taskId, string priority)
        {
            _logger.LogInformation("Updating priority of task {TaskId} to {Priority}", taskId, priority);

            // ✅ Fetch the task and update timestamp
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return false;

            task.Priority = priority;
            task.UpdatedAt = DateTime.UtcNow; // ✅ Update timestamp

            await _taskRepository.UpdateTaskAsync(task);
            return true;
        }
    }
}
