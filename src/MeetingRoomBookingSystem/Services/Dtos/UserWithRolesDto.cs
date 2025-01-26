namespace Services.Dtos
{
    public class UserWithRolesDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
