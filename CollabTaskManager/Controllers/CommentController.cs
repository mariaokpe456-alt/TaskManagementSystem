using CollabTaskManager.Models.DTOs;
using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollabTaskManager.Controllers
{
    [Authorize]
    [Route("api/tasks/{taskId}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid taskId)
        {
            var comments = await _commentRepository.GetCommentsByTaskIdAsync(taskId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid taskId, [FromBody] CommentDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var comment = new Comment
            {
                TaskId = taskId,
                UserId = userId,
                Text = model.Text
            };

            var addedComment = await _commentRepository.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetComments), new { taskId = addedComment.TaskId }, addedComment);
        }
    }
}

