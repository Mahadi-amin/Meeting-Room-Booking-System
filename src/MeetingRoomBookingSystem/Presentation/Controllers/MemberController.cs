using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

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
                    Id = user.Id,
                    UserName = user.Name,
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

        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> CreateRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                TempData["Error"] = "Role name cannot be empty!";
                return RedirectToAction("CreateRole");
            }

            var role = new ApplicationRole
            {
                Name = roleName
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = $"Role '{roleName}' created successfully!";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    TempData["Error"] = error.Description;
                }
            }

            return RedirectToAction("CreateRole");
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                TempData["Error"] = "Role not found!";
                return RedirectToAction("CreateRole");
            }

            return View(role); 
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(ApplicationRole role)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                TempData["Error"] = "Role name cannot be empty!";
                return RedirectToAction("CreateRole");
            }

            var existingRole = await _roleManager.FindByIdAsync(role.Id.ToString());
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                var result = await _roleManager.UpdateAsync(existingRole);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Role updated successfully!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        TempData["Error"] = error.Description;
                    }
                }
            }

            return RedirectToAction("CreateRole");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Role deleted successfully!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        TempData["Error"] = error.Description;
                    }
                }
            }

            return RedirectToAction("CreateRole");
        }

    }
}
