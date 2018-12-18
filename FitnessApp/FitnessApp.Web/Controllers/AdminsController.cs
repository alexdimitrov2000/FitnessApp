namespace FitnessApp.Web.Controllers
{
    using Common.Constants;
    using FitnessApp.Services.Contracts;
    using FitnessApp.Web.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = RolesConstants.ADMINISTRATOR_ROLE)]
    public class AdminsController : Controller
    {
        private readonly IUsersService users;

        public AdminsController(IUsersService users)
        {
            this.users = users;
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
    }
}
