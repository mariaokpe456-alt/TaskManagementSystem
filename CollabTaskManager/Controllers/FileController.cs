using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabTaskManager.Controllers
{
    [Route("api/tasks/{taskId}/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(Guid taskId, IFormFile file)
        {
            var filePath = $"uploads/{file.FileName}";

            using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);

            var taskFile = new TaskFile { TaskId = taskId, FilePath = filePath };
            var uploadedFile = await _fileRepository.UploadFileAsync(taskFile);

            return Ok(uploadedFile);
        }
    }
}

