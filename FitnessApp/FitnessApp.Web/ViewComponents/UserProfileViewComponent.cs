namespace FitnessApp.Web.ViewComponents
{
    using FitnessApp.Models;
    using FitnessApp.Services.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using System.Security.Claims;
    using System.Threading.Tasks;

    [ViewComponent(Name = "UserProfile")]
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly UserManager<FitnessUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public UserProfileViewComponent(UserManager<FitnessUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal principalUser)
        {
            var user = await this.userManager.GetUserAsync(principalUser);

            var profilePictureUrl = this.cloudinaryService.BuildPictureUrl(user.ProfilePicture);

            return this.View((object)profilePictureUrl);
        }
    }
}
