//using CollabTaskManager.Models;

//namespace CollabTaskManager.Services.Interfaces
//{
//    public interface IProjectRepository
//    {
//        Task<IEnumerable<Project>> GetAllProjectsAsync();
//        Task<Project?> GetProjectByIdAsync(Guid id);
//        Task<Project> CreateProjectAsync(Project project);
//        Task<Project> UpdateProjectAsync(Project project);
//        Task<bool> DeleteProjectAsync(Guid id);
//    }
//}


using CollabTaskManager.Models;

namespace CollabTaskManager.Services.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(Guid id);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Guid id);
    }
}
