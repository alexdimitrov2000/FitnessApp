namespace FitnessApp.Web.Models.Foods
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class AddFoodInputModel
    {
        [Required]
        [StringLength(ValidationConstants.MAX_FOOD_NAME, MinimumLength = ValidationConstants.MIN_FOOD_NAME)]
        public string Name { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Fats { get; set; }
        
    }
}
