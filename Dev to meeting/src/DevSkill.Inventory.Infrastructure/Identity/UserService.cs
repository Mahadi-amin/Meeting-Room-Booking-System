using DevSkill.Inventory.Infrastructure.DTOs;
using DevSkill.Inventory.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<UserWithRolesDto>> GetAllUsersAsync()
        {
            var usersWithRoles = _context.Users
            .Select(u => new UserWithRolesDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    RoleNames = _userManager.GetRolesAsync(u).Result.ToList() 
                })
                .ToList(); 

            return usersWithRoles;

        }

        public ApplicationUser GetUserById(Guid id)
        {
            return _context.Users.Find(id); 
        }

        public void CreateUser(ApplicationUser user)
        {
            _userManager.CreateAsync(user).Wait(); 
        }

        public void UpdateUser(ApplicationUser user)
        {
            _userManager.UpdateAsync(user).Wait(); 
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id); 
            if (user != null)
            {
                _userManager.DeleteAsync(user).Wait(); 
            }
        }
    }
}
