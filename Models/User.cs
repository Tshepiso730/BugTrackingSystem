// User.cs

using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Models
{
    public class User : IdentityUser
    {
        // Additional properties as needed for user management
        public string FullName { get; set; }
    }
}
