namespace FitnessApp.Models
{
    using Common.Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Food
    {
        public Food()
        {
            this.Users = new List<UserFood>();
            this.DiaryFood = new List<DiaryFood>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MAX_FOOD_NAME, MinimumLength = ValidationConstants.MIN_FOOD_NAME)]
        public string Name { get; set; }

        public decimal Calories => 4 * this.Protein + 4 * this.Carbohydrates + 9 * this.Fats;

        public decimal Protein { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Fats { get; set; }

        public virtual ICollection<UserFood> Users { get; set; }

        public virtual ICollection<DiaryFood> DiaryFood { get; set; }
    }
}
