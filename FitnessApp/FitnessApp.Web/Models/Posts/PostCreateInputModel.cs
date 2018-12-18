namespace FitnessApp.Web.Models.Posts
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class PostCreateInputModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
