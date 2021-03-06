﻿namespace FitnessApp.Web.Controllers
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

        [Route(nameof(LoadComments))]
        public async Task<IActionResult> LoadComments(LoadCommentsInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentsData = await this.comments.LoadCommentsAsync(model.PageSize, model.CurrentPage, model.PostId);

            if(commentsData == null)
            {
                return NotFound();
            }

            var result = new JsonResult(commentsData);

            return result;
        }

        [HttpPost]
        [Route(nameof(AddComment))]
        public async Task<IActionResult> AddComment([FromBody] CommentInputModel model)
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
        [Route(nameof(RemoveComment) + "/{commentId}")]
        public async Task<IActionResult> RemoveComment(int commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await this.comments.RemoveCommentAsync(commentId,User.Identity.Name);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }
    }

}
