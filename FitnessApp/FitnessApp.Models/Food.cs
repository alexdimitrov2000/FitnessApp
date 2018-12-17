namespace FitnessApp.Models
{
    using FitnessApp.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Food
    {
        public Food()
        {
            this.Users = new List<UserFood>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MAX_FOOD_NAME, MinimumLength = ValidationConstants.MIN_FOOD_NAME)]
        public string Name { get; set; }

        public decimal Calories => 4 * this.Protein + 4 * this.Carbohydrates + 9 * this.Fats;

        public decimal Protein { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Fats { get; set; }

        public ICollection<UserFood> Users { get; set; }
    }
}
