namespace FitnessApp.Web.Controllers
{
    using Models.Posts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    
    public class PostsController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateInputModel model)
        {
            var imageFile = model.Image;

            //return this.RedirectToAction("Details", "Posts", new { id = post.Id });
            return this.View();
        }
    }
}
