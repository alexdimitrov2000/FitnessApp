namespace FitnessApp.Models
{
    public class Like
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public FitnessUser User { get; set; }
    }
}
