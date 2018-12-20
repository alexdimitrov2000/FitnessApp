namespace FitnessApp.Web.Controllers
{
    using Models.Home;
    using Common.Constants;
    using Services.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using FitnessApp.Models;
    using Microsoft.AspNetCore.Identity;
    using FitnessApp.Web.Models.Posts;
    
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

        [Authorize(Roles = RolesConstants.ADMINISTRATOR_ROLE)]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = RolesConstants.ADMINISTRATOR_ROLE)]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var imageFile = model.Image;
            var postTitle = model.Title;
            var image = await this.cloudinaryService.UploadImageAsync(typeof(Post), imageFile);

            await this.postsService.CreateAsync(postTitle, model.Content, image, model.CategoryId, User.Identity.Name);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var post = await this.postsService.GetByIdAsync(id);
            var currentUsername = User.Identity.Name;
            var currentUser = await this.userManager.FindByNameAsync(currentUsername);

            if (post == null)
                return this.NotFound();

            var postViewModel = new PostDetailsViewModel
            {
                Id = post.Id,
                CategoryName = post.Category.Name,
                Content = post.Content,
                UserName = post.User.UserName,
                Title = post.Title,
                ImageUrl = this.cloudinaryService.BuildPostPictureUrl(post.Image),
                Likes = post.Likes,
                Comments = post.Comments,
                //CurrentUserProfilePicture = this.cloudinaryService.BuildPostPictureUrl(currentUser.ProfilePicture),
                IsLiked = await this.postsService.IsLikedAsync(currentUsername, post.Id)
            };

            return this.View(postViewModel);
        }
    }
}
