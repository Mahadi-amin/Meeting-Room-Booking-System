using DataAccess.Data;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Identity
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
            // Step 1: Fetch users asynchronously.
            var users = await _context.Users.ToListAsync();

            // Step 2: Fetch roles asynchronously for each user.
            var usersWithRoles = new List<UserWithRolesDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                usersWithRoles.Add(new UserWithRolesDto
                {
                    UserName = user.UserName,
                    RoleNames = roles.ToList()  // Store roles
                });
            }

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
