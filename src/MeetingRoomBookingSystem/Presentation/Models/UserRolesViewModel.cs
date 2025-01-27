namespace Presentation.Models
{
    public class UserRolesViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public bool? Status { get; set; }
        
        public IEnumerable<string> Roles { get; set; }
    }

}
