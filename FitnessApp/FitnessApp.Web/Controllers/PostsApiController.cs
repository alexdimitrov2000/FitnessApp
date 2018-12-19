namespace FitnessApp.Web.Controllers
{
    using Web.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services.Contracts;
    using System;

    [Route("api/posts")]
    public class PostsApiController : Controller
    {
        private readonly IPostsService posts;

        public PostsApiController(IPostsService posts)
        {
            this.posts = posts;
        }

        [HttpPost]
        [Route(nameof(AddLike))]
        public async Task<IActionResult> AddLike(LikeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await this.posts.AddLikeAsync(model.Username, model.PostId);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpPost]
        [Route(nameof(RemoveLike))]
        public async Task<IActionResult> RemoveLike(LikeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await this.posts.RemoveLikeAsync(model.Username, model.PostId);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
