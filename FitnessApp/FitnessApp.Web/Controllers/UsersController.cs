namespace FitnessApp.Web.Controllers
{
    using Web.Models.Users;
    using FitnessApp.Models;
    using Services.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using System.Threading.Tasks;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<FitnessUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public UsersController(UserManager<FitnessUser> userManager, ICloudinaryService cloudinaryService, IUsersService users)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.users = users;
        }
        
        [Route(nameof(Profile) + "/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            //TODO add deactivated view
            if (!user.IsActive)
            {
                return NotFound();
            }

            var userPosts = user.Posts.Select(p => new UserPostViewModel
            {
                Id = p.Id,
                PostPictureUrl = this.cloudinaryService.BuildPictureUrl(p.Image),
                Title = p.Title
            }).ToList();

            var userViewModel = new UserProfileViewModel
            {
                BodyFat = user.BodyFat ?? 0,
                DateOfBirth = user.BirthDate == null ? "Birth date is not set." : user.BirthDate?.ToShortDateString(),
                Email = user.Email,
                Height = user.Height ?? 0,
                ProfilePictureUrl = this.cloudinaryService.BuildPictureUrl(user.ProfilePicture),
                UserName = user.UserName,
                Weight = user.Weight ?? 0,
                Posts = userPosts
            };

            return this.View(userViewModel);
        }

        public async Task<IActionResult> AddGoal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal(GoalInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var username = User.Identity.Name;
    
            var success = await this.users.AddGoal(model.Weight, model.Height, model.Age, model.Gender, model.ActivityLevel, model.WeightChangeType, username);

            if (!success)
            {
                return BadRequest();
            }

            var user = await this.userManager.FindByNameAsync(username);
            
            return View("Calculated", user.Goals.FirstOrDefault());
        }
    }
}
