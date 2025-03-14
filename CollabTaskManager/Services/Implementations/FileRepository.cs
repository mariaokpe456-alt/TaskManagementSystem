//using CollabTaskManager.Data;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class FileRepository : IFileRepository
//    {
//        private readonly AppDbContext _context;

//        public FileRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<TaskFile> UploadFileAsync(TaskFile file)
//        {
//            _context.TaskFiles.Add(file);
//            await _context.SaveChangesAsync();
//            return file;
//        }

//        public async Task<IEnumerable<TaskFile>> GetFilesByTaskIdAsync(Guid taskId)
//        {
//            return await _context.TaskFiles.Where(f => f.TaskId == taskId).ToListAsync();
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
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FileRepository> _logger;

        public FileRepository(AppDbContext context, ILogger<FileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskFile> UploadFileAsync(TaskFile file)
        {
            try
            {
                _logger.LogInformation("Uploading a new file with ID: {FileId} for Task ID: {TaskId}", file.Id, file.TaskId);

                _context.TaskFiles.Add(file);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully uploaded file with ID: {FileId}", file.Id);
                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file for Task ID: {TaskId}", file.TaskId);
                throw new Exception("An error occurred while uploading the file.");
            }
        }

        public async Task<IEnumerable<TaskFile>> GetFilesByTaskIdAsync(Guid taskId)
        {
            try
            {
                _logger.LogInformation("Fetching files for Task ID: {TaskId}", taskId);

                var files = await _context.TaskFiles
                    .Where(f => f.TaskId == taskId)
                    .ToListAsync();

                _logger.LogInformation("Successfully retrieved {Count} files for Task ID: {TaskId}", files.Count, taskId);
                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving files for Task ID: {TaskId}", taskId);
                throw new Exception("An error occurred while retrieving files.");
            }
        }
    }
}
