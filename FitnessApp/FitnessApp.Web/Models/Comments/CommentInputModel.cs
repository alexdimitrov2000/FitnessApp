namespace FitnessApp.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class CommentInputModel
    {
        [Required]
        [MinLength(ValidationConstants.MIN_COMMENT_CONTENT)]
        public string Content { get; set; }

        [Required]
        [StringLength(ValidationConstants.MAX_USER_NAME, MinimumLength = ValidationConstants.MIN_USER_NAME)]
        public string Username { get; set; }

        [Required]
        public int PostId { get; set; }

        public int CommentId { get; set; }
    }
}
