namespace FitnessApp.Models
{
    public class UserFood
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public string UserId { get; set; }
        public FitnessUser User { get; set; }
    }
}
