namespace FitnessApp.Web.Models.Home
{
    using FitnessApp.Models;

    public class PostIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public Image Image { get; set; }
    }
}
