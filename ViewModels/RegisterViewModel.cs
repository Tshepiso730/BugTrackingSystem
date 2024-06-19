namespace BugTrackingSystem.ViewModels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Role selection based on specific users
        public bool IsAdmin { get; set; } // Role is for Tshepiso Mathlore
        public bool IsRD { get; set; }   // Role is for Tshepang Mathlore
        public bool IsPM { get; set; }   // Role is for Jessie Mathlore
    }
}
