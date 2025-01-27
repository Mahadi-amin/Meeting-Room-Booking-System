using DataAccess;
using DataAccess.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using System.Web;

namespace Presentation.Controllers
{
    public class MemberController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public MemberController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }


        public async Task<IActionResult> GetUserWithRoles()
        {
            var users = _userManager.Users.ToList();
            var usersWithRoles = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                usersWithRoles.Add(new UserRolesViewModel
                {
                    UserName = user.UserName,
                    Pin = user.Pin,            
                    Email = user.Email,        
                    Phone = user.PhoneNumber,  
                    Department = user.Department,
                    Designation = user.Designation, 
                    Status = user.Status,  
                    Roles = roles
                });
            }

            return View(usersWithRoles);
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

    }
}
