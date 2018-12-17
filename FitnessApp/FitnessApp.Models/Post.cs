namespace FitnessApp.Models
{
    using FitnessApp.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
            this.Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MAX_POST_TITLE, MinimumLength = ValidationConstants.MIN_POST_TITLE)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.MIN_POST_CONTENT)]
        public string Content { get; set; }

        public Image Image { get; set; }

        public string UserId { get; set; }
        public FitnessUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
