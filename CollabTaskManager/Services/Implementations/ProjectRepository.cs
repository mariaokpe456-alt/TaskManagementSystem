//using CollabTaskManager.Data;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class ProjectRepository : IProjectRepository
//    {
//        private readonly AppDbContext _context;

//        public ProjectRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
//        {
//            return await _context.Projects.ToListAsync();
//        }

//        public async Task<Project?> GetProjectByIdAsync(Guid id)
//        {
//            return await _context.Projects.FindAsync(id);
//        }

//        public async Task<Project> CreateProjectAsync(Project project)
//        {
//            _context.Projects.Add(project);
//            await _context.SaveChangesAsync();
//            return project;
//        }

//        public async Task<Project> UpdateProjectAsync(Project project)
//        {
//            _context.Projects.Update(project);
//            await _context.SaveChangesAsync();
//            return project;
//        }

//        public async Task<bool> DeleteProjectAsync(Guid id)
//        {
//            var project = await _context.Projects.FindAsync(id);
//            if (project == null) return false;

//            _context.Projects.Remove(project);
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(AppDbContext context, ILogger<ProjectRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all projects from the database.");
                return await _context.Projects.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects.");
                throw new Exception("An error occurred while retrieving projects.");
            }
        }

        public async Task<Project?> GetProjectByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching project with ID: {ProjectId}", id);
                var project = await _context.Projects.FindAsync(id);

                if (project == null)
                {
                    _logger.LogWarning("Project with ID {ProjectId} not found.", id);
                }

                return project;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project with ID: {ProjectId}", id);
                throw new Exception("An error occurred while retrieving the project.");
            }
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            try
            {
                _logger.LogInformation("Creating a new project: {ProjectName}", project.Name);
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project created successfully with ID: {ProjectId}", project.Id);
                return project;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project: {ProjectName}", project.Name);
                throw new Exception("An error occurred while creating the project.");
            }
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            try
            {
                _logger.LogInformation("Updating project with ID: {ProjectId}", project.Id);
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project updated successfully: {ProjectId}", project.Id);
                return project;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project with ID: {ProjectId}", project.Id);
                throw new Exception("An error occurred while updating the project.");
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete project with ID: {ProjectId}", id);
                var project = await _context.Projects.FindAsync(id);

                if (project == null)
                {
                    _logger.LogWarning("Project with ID {ProjectId} not found for deletion.", id);
                    return false;
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project deleted successfully: {ProjectId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project with ID: {ProjectId}", id);
                throw new Exception("An error occurred while deleting the project.");
            }
        }
    }
}
