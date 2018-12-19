namespace FitnessApp.Web.Models.Home
{
    using FitnessApp.Common.Constants;
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class PostCreateInputModel
    {
        [Required]
        [StringLength(ValidationConstants.MAX_POST_TITLE, MinimumLength = ValidationConstants.MIN_POST_TITLE)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.MIN_POST_CONTENT)]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
