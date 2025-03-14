//using CollabTaskManager.Models.DTOs;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using CollabTaskManager.Services.Implementations;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace CollabTaskManager.Controllers
//{
//    [Authorize]
//    [Route("api/tasks")]
//    [ApiController]
//    public class TaskController : ControllerBase
//    {
//        private readonly ITaskRepository _taskRepository;
//        private readonly INotificationService _notificationService; // ✅ FIXED: Added Notification Service

//        public TaskController(ITaskRepository taskRepository, INotificationService notificationService)
//        {
//            _taskRepository = taskRepository;
//            _notificationService = notificationService; // ✅ Assign Notification Service
//        }

//        // ✅ GET: Retrieve tasks for a specific project
//        [HttpGet("project/{projectId}")]
//        public async Task<IActionResult> GetTasksByProject(Guid projectId)
//        {
//            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
//            return Ok(tasks);
//        }

//        // ✅ GET: Retrieve a task by ID (Fix for CreatedAtAction reference)
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetTaskById(Guid id)
//        {
//            var task = await _taskRepository.GetTaskByIdAsync(id);
//            if (task == null) return NotFound();

//            return Ok(task);
//        }

//        // ✅ POST: Create a new task
//        [HttpPost]
//        public async Task<IActionResult> CreateTask([FromBody] TaskDto model)
//        {
//            var task = new TaskItem
//            {
//                Title = model.Title,
//                Description = model.Description,
//                ProjectId = model.ProjectId
//            };

//            var createdTask = await _taskRepository.CreateTaskAsync(task);

//            // ✅ Send real-time notification
//            await _notificationService.NotifyTaskUpdated($"New Task Created: {createdTask.Title}");

//            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
//        }

//        // ✅ PUT: Update task status and priority
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskUpdateDto model)
//        {
//            var task = await _taskRepository.GetTaskByIdAsync(id);
//            if (task == null) return NotFound();

//            task.Status = model.Status;
//            task.Priority = model.Priority;

//            var updatedTask = await _taskRepository.UpdateTaskAsync(task);
//            return Ok(updatedTask);
//        }

//        // ✅ PUT: Assign a task to a user
//        [HttpPut("{id}/assign")]
//        public async Task<IActionResult> AssignTask(Guid id, [FromBody] TaskAssignmentDto model)
//        {
//            var success = await _taskRepository.AssignTaskAsync(id, model.UserId);
//            if (!success) return NotFound();

//            return Ok(new { message = "Task assigned successfully" });
//        }

//        // ✅ DELETE: Delete a task
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTask(Guid id)
//        {
//            var success = await _taskRepository.DeleteTaskAsync(id);
//            if (!success) return NotFound();

//            return NoContent();
//        }
//    }
//}

using CollabTaskManager.Models.DTOs;
using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollabTaskManager.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly INotificationService _notificationService;

        public TaskController(ITaskService taskService, INotificationService notificationService)
        {
            _taskService = taskService;
            _notificationService = notificationService;
        }

        // ✅ GET: Retrieve tasks for a specific project
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        // ✅ GET: Retrieve a task by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        // ✅ POST: Create a new task
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto model)
        {
            var task = new TaskItem
            {
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId
            };

            var createdTask = await _taskService.CreateTaskAsync(task);

            // ✅ Send real-time notification
            await _notificationService.NotifyTaskUpdated($"New Task Created: {createdTask.Title}");

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // ✅ PUT: Update task status and priority
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskUpdateDto model)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            task.Status = model.Status;
            task.Priority = model.Priority;

            var updatedTask = await _taskService.UpdateTaskAsync(task);
            return Ok(updatedTask);
        }

        // ✅ PUT: Assign a task to a user
        [HttpPut("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid id, [FromBody] TaskAssignmentDto model)
        {
            var success = await _taskService.AssignTaskAsync(id, model.UserId);
            if (!success) return NotFound();

            return Ok(new { message = "Task assigned successfully" });
        }

        // ✅ DELETE: Delete a task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
