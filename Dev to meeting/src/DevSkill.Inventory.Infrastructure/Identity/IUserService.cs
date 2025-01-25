using DevSkill.Inventory.Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public interface IUserService
    {
        Task<IEnumerable<UserWithRolesDto>> GetAllUsersAsync();
        ApplicationUser GetUserById(Guid id);      
        void CreateUser(ApplicationUser user);     
        void UpdateUser(ApplicationUser user);     
        void DeleteUser(Guid id);
    }
}
