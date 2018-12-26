namespace FitnessApp.Web.Models.Users
{
    using FitnessApp.Models.Enums;

    public class GoalInputModel
    {
        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public ActivityLevel ActivityLevel { get; set; }

        public WeightChangeType WeightChangeType { get; set; }
        
    }
}
