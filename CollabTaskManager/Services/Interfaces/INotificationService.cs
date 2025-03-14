using System.Threading.Tasks;
namespace CollabTaskManager.Services.Interfaces
{
    public interface INotificationService
    {
        Task NotifyTaskUpdated(string message);
    }
}
