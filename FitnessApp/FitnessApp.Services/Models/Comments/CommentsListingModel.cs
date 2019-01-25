using FitnessApp.Models;

namespace FitnessApp.Services.Models.Comments
{
    public class CommentsListingModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
