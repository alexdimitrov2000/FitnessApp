namespace FitnessApp.Web.Models.Categories
{
    using Common.Constants;

    using System.ComponentModel.DataAnnotations;

    public class CategoryCreateInputModel
    {
        [Required]
        [StringLength(ValidationConstants.MAX_CATEGORY_NAME, MinimumLength = ValidationConstants.MIN_CATEGORY_NAME)]
        public string Name { get; set; }
    }
}
