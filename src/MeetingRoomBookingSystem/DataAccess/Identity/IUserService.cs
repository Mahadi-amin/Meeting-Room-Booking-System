using DataAccess.DTOs;

namespace DataAccess.Identity
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
