namespace FitnessApp.Models
{
    using System;
    using System.Collections.Generic;

    public class FoodDiary
    {
        public FoodDiary()
        {
            this.Meals = new List<DiaryFood>();
        }

        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual FitnessUser User { get; set; }

        public virtual ICollection<DiaryFood> Meals { get; set; }
    }
}
