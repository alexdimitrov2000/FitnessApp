namespace FitnessApp.Web.Models.Foods
{
    using FitnessApp.Models;
    using System;
    using System.Collections.Generic;

    public class FoodDiaryViewModel
    {
        public int Id { get; set; }

        public decimal EatenCalories { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual FitnessUser User { get; set; }

        public virtual Goal Goal { get; set; }

        public virtual ICollection<DiaryFood> Meals { get; set; }
    }
}
