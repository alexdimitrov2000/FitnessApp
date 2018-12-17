﻿namespace FitnessApp.Models
{
    using System;
    using System.Collections.Generic;

    public class FoodDiary
    {
        public FoodDiary()
        {
            this.Meals = new List<Food>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public FitnessUser User { get; set; }

        public ICollection<Food> Meals { get; set; }
    }
}