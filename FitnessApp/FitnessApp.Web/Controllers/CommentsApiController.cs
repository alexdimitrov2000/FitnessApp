namespace FitnessApp.Web.Controllers
{
    using Models.Comments;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services.Contracts;

    [Route("api/comments")]
    public class CommentsApiController : Controller
    {
        private readonly ICommentsService comments;

        public CommentsApiController(ICommentsService comments)
        {
            this.comments = comments;
        }

        [HttpPost]
        [Route(nameof(AddComment))]
        public async Task<IActionResult> AddComment(CommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await this.comments.AddCommentAsync(model.Content, model.Username, model.PostId);

            if (!success)
            {
                return BadRequest();
            }

            return Ok(model);
        }

        [HttpPost]
        [Route(nameof(RemoveComment))]
        public async Task<IActionResult> RemoveComment(CommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await this.comments.RemoveCommentAsync(model.CommentId,model.Username);

            if (!success)
            {
                return BadRequest();
            }

            return Ok(model);
        }
    }

}
