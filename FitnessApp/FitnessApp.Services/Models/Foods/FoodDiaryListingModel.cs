namespace FitnessApp.Services.Models.Foods
{
    using FitnessApp.Models;
    using System;
    using System.Collections.Generic;

    public class FoodDiaryListingModel
    {
        public int Id { get; set; }

        public decimal EatenCalories { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual FitnessUser User { get; set; }

        public virtual ICollection<Food> Meals { get; set; }
    }
}
