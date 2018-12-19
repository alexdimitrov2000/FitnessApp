namespace FitnessApp.Models
{
    using Common.Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Posts = new List<Post>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MAX_CATEGORY_NAME, MinimumLength = ValidationConstants.MIN_CATEGORY_NAME)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
