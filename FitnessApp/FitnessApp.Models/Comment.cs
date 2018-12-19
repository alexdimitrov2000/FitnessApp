namespace FitnessApp.Models
{
    using Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public Comment()
        {
            this.Replies = new List<Reply>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MIN_COMMENT_CONTENT)]
        public string Content { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public string UserId { get; set; }
        public virtual FitnessUser User { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
