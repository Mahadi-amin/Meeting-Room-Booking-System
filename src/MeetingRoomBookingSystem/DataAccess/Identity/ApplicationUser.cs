using Microsoft.AspNetCore.Identity;

namespace DataAccess.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Pin { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public bool? Status { get; set; } = false;

    }
}
