namespace FitnessApp.Web.Models.Posts
{
    using FitnessApp.Models;
    using System.Collections.Generic;

    public class PostDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public string UserName { get; set; }

        public string ImageUrl { get; set; }

        public bool IsLiked { get; set; }

        public string CurrentUserProfilePicture { get; set; }

        public IEnumerable<Like> Likes { get; set; } = new List<Like>();
        
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
