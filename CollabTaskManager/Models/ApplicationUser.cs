using Microsoft.AspNetCore.Identity;

namespace CollabTaskManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}

