namespace FitnessApp.Web.Controllers
{
    using Models.Posts;
    using Common.Constants;
    using Services.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using FitnessApp.Models;
    using Microsoft.AspNetCore.Identity;

    [Authorize(Roles = RolesConstants.ADMINISTRATOR_ROLE)]
    public class PostsController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPostsService postsService;
        private readonly UserManager<FitnessUser> userManager;

        public PostsController(ICloudinaryService cloudinaryService, IPostsService postsService, UserManager<FitnessUser> userManager)
        {
            this.cloudinaryService = cloudinaryService;
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateInputModel model)
        {
            // TODO: Not ready, Finalize!
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var imageFile = model.Image;
            var image = await this.cloudinaryService.UploadImageAsync(typeof(Post), imageFile);

            await this.postsService.CreateAsync(model.Title, model.Content, image, model.CategoryId, this.userManager.GetUserName(this.User));

            //return this.RedirectToAction("Details", "Posts", new { id = post.Id });
            return this.View();
        }
    }
}
