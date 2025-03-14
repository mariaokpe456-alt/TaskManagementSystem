//using CollabTaskManager.Models.DTOs;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System.Security.Claims;

//namespace CollabTaskManager.Controllers
//{
//    [Authorize]
//    [Route("api/projects")]
//    [ApiController]
//    public class ProjectController : ControllerBase
//    {
//        private readonly IProjectRepository _projectRepository;
//        private readonly ILogger<ProjectController> _logger;

//        public ProjectController(IProjectRepository projectRepository, ILogger<ProjectController> logger)
//        {
//            _projectRepository = projectRepository;
//            _logger = logger;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllProjects()
//        {
//            var projects = await _projectRepository.GetAllProjectsAsync();
//            return Ok(projects);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetProjectById(Guid id)
//        {
//            var project = await _projectRepository.GetProjectByIdAsync(id);
//            if (project == null)
//            {
//                _logger.LogWarning("Project with ID {ProjectId} not found.", id);
//                return NotFound(new { message = "Project not found" });
//            }
//            return Ok(project);
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        public async Task<IActionResult> CreateProject([FromBody] ProjectDto model)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null) return Unauthorized(new { message = "User not authenticated" });

//            var project = new Project
//            {
//                Name = model.Name,
//                Description = model.Description,
//                CreatedBy = userId
//            };

//            try
//            {
//                var createdProject = await _projectRepository.CreateProjectAsync(project);
//                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating project");
//                return StatusCode(500, new { message = "An error occurred while creating the project." });
//            }
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto model)
//        {
//            var project = await _projectRepository.GetProjectByIdAsync(id);
//            if (project == null)
//            {
//                _logger.LogWarning("Project with ID {ProjectId} not found for update.", id);
//                return NotFound(new { message = "Project not found" });
//            }

//            project.Name = model.Name;
//            project.Description = model.Description;

//            try
//            {
//                var updatedProject = await _projectRepository.UpdateProjectAsync(project);
//                return Ok(updatedProject);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating project with ID {ProjectId}", id);
//                return StatusCode(500, new { message = "An error occurred while updating the project." });
//            }
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProject(Guid id)
//        {
//            try
//            {
//                var success = await _projectRepository.DeleteProjectAsync(id);
//                if (!success)
//                {
//                    _logger.LogWarning("Project with ID {ProjectId} not found for deletion.", id);
//                    return NotFound(new { message = "Project not found" });
//                }
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting project with ID {ProjectId}", id);
//                return StatusCode(500, new { message = "An error occurred while deleting the project." });
//            }
//        }
//    }
//}




using CollabTaskManager.Models.DTOs;
using CollabTaskManager.Models;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollabTaskManager.Controllers
{
    [Authorize]
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null) return NotFound(new { message = "Project not found" });

            return Ok(project);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatedBy = userId!
            };

            var createdProject = await _projectRepository.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto model)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null) return NotFound(new { message = "Project not found" });

            project.Name = model.Name;
            project.Description = model.Description;

            var updatedProject = await _projectRepository.UpdateProjectAsync(project);
            return Ok(updatedProject);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var success = await _projectRepository.DeleteProjectAsync(id);
            if (!success) return NotFound(new { message = "Project not found" });

            return NoContent();
        }
    }
}
