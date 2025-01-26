namespace Presentation.Models
{
    public class UserRolesViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
