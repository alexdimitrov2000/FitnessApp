namespace FitnessApp.Models
{
    using Enums;
    using System;

    public class Goal
    {
        public int Id { get; set; }

        public ActivityLevel ActivityLevel { get; set; }

        public WeightChangeType WeightChange { get; set; }

        public DateTime Date { get; set; }

        public decimal Calories { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Fats { get; set; }

        public virtual FitnessUser User { get; set; }

        public string UserId { get; set; }
    }
}
