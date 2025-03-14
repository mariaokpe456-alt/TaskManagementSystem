using CollabTaskManager.Models.DTOs;

namespace CollabTaskManager.Services.Interfaces
{
    public interface IAuthService
    {
        //Task<string> RegisterAsync(RegisterDto model);
        Task<string> RegisterAsync(RegisterDto model, string role);
        Task<string> LoginAsync(LoginDto model);
    }
}
