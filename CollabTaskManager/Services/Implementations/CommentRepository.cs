//using CollabTaskManager.Data;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class CommentRepository : ICommentRepository
//    {
//        private readonly AppDbContext _context;

//        public CommentRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(Guid taskId)
//        {
//            return await _context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
//        }

//        public async Task<Comment> AddCommentAsync(Comment comment)
//        {
//            _context.Comments.Add(comment);
//            await _context.SaveChangesAsync();
//            return comment;
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
using System.Linq;
using System.Threading.Tasks;

namespace CollabTaskManager.Services.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(AppDbContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(Guid taskId)
        {
            try
            {
                _logger.LogInformation("Fetching comments for Task ID: {TaskId}", taskId);

                var comments = await _context.Comments
                    .Where(c => c.TaskId == taskId)
                    .ToListAsync();

                _logger.LogInformation("Successfully fetched {Count} comments for Task ID: {TaskId}", comments.Count, taskId);
                return comments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comments for Task ID: {TaskId}", taskId);
                throw new Exception("An error occurred while retrieving comments.");
            }
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            try
            {
                _logger.LogInformation("Adding a new comment for Task ID: {TaskId}", comment.TaskId);

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully added comment with ID: {CommentId}", comment.Id);
                return comment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment for Task ID: {TaskId}", comment.TaskId);
                throw new Exception("An error occurred while adding the comment.");
            }
        }
    }
}
