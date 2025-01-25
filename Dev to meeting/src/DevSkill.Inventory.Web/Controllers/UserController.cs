using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult RegisterFromCsv()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFromCsv(IFormFile file)
        {
            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".csv"))
            {
                ViewBag.Message = "Please upload a valid CSV file.";
                return View();
            }

            var errors = new List<string>();
            var successCount = 0;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var header = reader.ReadLine(); // Skip the header row
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line.Split(',');

                    if (fields.Length < 4) // Ensure all required fields are present
                    {
                        errors.Add("Invalid row format. Expected: FirstName, LastName, Email, Password, RoleName.");
                        continue;
                    }

                    var firstName = fields[0].Trim();
                    var lastName = fields[1].Trim();
                    var email = fields[2].Trim();
                    var password = fields[3].Trim();
                    var roleName = fields.Length > 4 ? fields[4].Trim() : "DefaultRole";

                    var user = new ApplicationUser
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = email,
                        Email = email
                    };

                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, roleName);
                        successCount++;
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            errors.Add($"Error for {email}: {error.Description}");
                        }
                    }
                }
            }

            ViewBag.Message = $"{successCount} users registered successfully.";
            if (errors.Any())
            {
                ViewBag.Errors = errors;
            }

            return View();
        }

    }
}
