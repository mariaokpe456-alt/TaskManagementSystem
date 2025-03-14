//using CollabTaskManager.Data;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class TaskRepository : ITaskRepository
//    {
//        private readonly AppDbContext _context;

//        public TaskRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId)
//        {
//            return await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
//        }

//        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
//        {
//            return await _context.Tasks.FindAsync(id);
//        }

//        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
//        {
//            _context.Tasks.Add(task);
//            await _context.SaveChangesAsync();
//            return task;
//        }

//        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
//        {
//            _context.Tasks.Update(task);
//            await _context.SaveChangesAsync();
//            return task;
//        }

//        public async Task<bool> DeleteTaskAsync(Guid id)
//        {
//            var task = await _context.Tasks.FindAsync(id);
//            if (task == null) return false;

//            _context.Tasks.Remove(task);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> AssignTaskAsync(Guid taskId, string userId)
//        {
//            var task = await _context.Tasks.FindAsync(taskId);
//            if (task == null) return false;

//            task.AssignedTo = userId;
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> UpdateTaskStatusAsync(Guid taskId, string status)
//        {
//            var task = await _context.Tasks.FindAsync(taskId);
//            if (task == null) return false;

//            task.Status = status;
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> UpdateTaskPriorityAsync(Guid taskId, string priority)
//        {
//            var task = await _context.Tasks.FindAsync(taskId);
//            if (task == null) return false;

//            task.Priority = priority;
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}


using CollabTaskManager.Data;
using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollabTaskManager.Services.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(AppDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(Guid projectId)
        {
            try
            {
                _logger.LogInformation("Fetching tasks for project ID: {ProjectId}", projectId);
                return await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tasks for project ID: {ProjectId}", projectId);
                throw new Exception("An error occurred while retrieving tasks.");
            }
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching task with ID: {TaskId}", id);
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found.", id);
                }

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task with ID: {TaskId}", id);
                throw new Exception("An error occurred while retrieving the task.");
            }
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            try
            {
                _logger.LogInformation("Creating a new task: {TaskTitle}", task.Title);
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task created successfully with ID: {TaskId}", task.Id);
                return task;
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error: {ex.Message}");
                _logger.LogInformation("Creating a new task: {TaskTitle}", task.Title);
                throw new Exception("An error occurred while creating the task.");
            }
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            try
            {
                _logger.LogInformation("Updating task with ID: {TaskId}", task.Id);
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task updated successfully: {TaskId}", task.Id);
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task with ID: {TaskId}", task.Id);
                throw new Exception("An error occurred while updating the task.");
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete task with ID: {TaskId}", id);
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for deletion.", id);
                    return false;
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task deleted successfully: {TaskId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with ID: {TaskId}", id);
                throw new Exception("An error occurred while deleting the task.");
            }
        }

        public async Task<bool> AssignTaskAsync(Guid taskId, string userId)
        {
            try
            {
                _logger.LogInformation("Assigning task {TaskId} to user {UserId}", taskId, userId);
                var task = await _context.Tasks.FindAsync(taskId);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for assignment.", taskId);
                    return false;
                }

                task.AssignedTo = userId;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task {TaskId} successfully assigned to user {UserId}", taskId, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning task {TaskId} to user {UserId}", taskId, userId);
                throw new Exception("An error occurred while assigning the task.");
            }
        }

        public async Task<bool> UpdateTaskStatusAsync(Guid taskId, string status)
        {
            try
            {
                _logger.LogInformation("Updating status of task {TaskId} to {Status}", taskId, status);
                var task = await _context.Tasks.FindAsync(taskId);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for status update.", taskId);
                    return false;
                }

                task.Status = status;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task {TaskId} status updated to {Status}", taskId, status);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status of task {TaskId} to {Status}", taskId, status);
                throw new Exception("An error occurred while updating the task status.");
            }
        }

        public async Task<bool> UpdateTaskPriorityAsync(Guid taskId, string priority)
        {
            try
            {
                _logger.LogInformation("Updating priority of task {TaskId} to {Priority}", taskId, priority);
                var task = await _context.Tasks.FindAsync(taskId);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for priority update.", taskId);
                    return false;
                }

                task.Priority = priority;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task {TaskId} priority updated to {Priority}", taskId, priority);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating priority of task {TaskId} to {Priority}", taskId, priority);
                throw new Exception("An error occurred while updating the task priority.");
            }
        }

    }
}
