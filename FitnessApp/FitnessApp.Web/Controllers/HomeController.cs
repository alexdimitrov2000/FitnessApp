namespace FitnessApp.Web.Controllers
{
    using Web.Models;
    using Services.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using FitnessApp.Web.Models.Home;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICloudinaryService cloudinaryService;

        public HomeController(IPostsService postsService, ICloudinaryService cloudinaryService)
        {
            this.postsService = postsService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await this.postsService.GetAllAsync();

            var postViewModels = posts.Select(p => new PostIndexViewModel { Id = p.Id, Title = p.Title, Image = p.Image }).ToList();
            postViewModels.ForEach(p => p.ImageUrl = this.cloudinaryService.BuildPictureUrl(p.Image));

            var postCollectionViewModel = new PostCollectionViewModel
            {
                Posts = postViewModels
            };

            return View(postCollectionViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
