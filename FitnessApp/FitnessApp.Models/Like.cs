namespace FitnessApp.Models
{
    public class Like
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public string UserId { get; set; }
        public virtual FitnessUser User { get; set; }
    }
}
