namespace FitnessApp.Models
{
    public class UserFood
    {
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }

        public string UserId { get; set; }
        public virtual FitnessUser User { get; set; }
    }
}
