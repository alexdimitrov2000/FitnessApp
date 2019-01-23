namespace FitnessApp.Models
{
    public class DiaryFood
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        public virtual Food Food { get; set; }

        public decimal Multiplier { get; set; }

        public int FoodDiaryId { get; set; }

        public virtual FoodDiary FoodDiary { get; set; }
        
    }
}
