namespace FitnessApp.Web.Controllers
{
    using Common.Constants;
    using FitnessApp.Models;
    using FitnessApp.Services.Contracts;
    using FitnessApp.Web.Models.Admins;
    using FitnessApp.Web.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = RolesConstants.ADMINISTRATOR_ROLE)]
    public class AdminsController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<FitnessUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminsController(IUsersService users, RoleManager<IdentityRole> roleManager, UserManager<FitnessUser> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllUsers()
        {
            var usersData = await this.users.AllUsersAsync();

            var usersModel = usersData.Select(u => new UsersListingViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive
            });

            return View(new AllUsersListingViewModel { AllUsers = usersModel });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserStatus(string id, bool isActive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = false;

            if (isActive)
            {
                success = await this.users.DeactivateUserAsync(id);
            }
            else
            {
                success = await this.users.ActivateUserAsync(id);
            }

            if (!success)
            {
                return BadRequest();
            }
            else
            {
                TempData["Success"] = "Successfully changed user status!";
            }

            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> AddToRole(string id)
        {
            var rolesSelectListItem = this.roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return View(rolesSelectListItem);

        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {

                return NotFound();
            }

            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                return BadRequest();
            }

            await this.userManager.AddToRoleAsync(user, role);

            TempData["Success"] = $"User {user.Name} successfully added to role {role}";

            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new ChangePasswordInputModel { Email = user.Email });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = $"Successfully changed password for user: {user.UserName}";

                return RedirectToAction(nameof(AllUsers));
            }
            else
            {
                this.AddModelErrors(result);

                return View(model);
            }
        }

        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
