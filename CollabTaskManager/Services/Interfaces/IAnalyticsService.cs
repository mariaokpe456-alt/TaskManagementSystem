using System.Threading.Tasks;
namespace CollabTaskManager.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<string> GetAnalyticsReportAsync();
        Task<Dictionary<string, int>> GetTaskStatusCount();
        Task<Dictionary<string, int>> GetTasksPerUser();
        Task<Dictionary<string, int>> GetCompletionTrends();
        Task<Dictionary<string, int>> GetTeamProductivityMetrics();
    }
}
