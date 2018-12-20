namespace FitnessApp.Web.Models.Users
{
    using System.Collections.Generic;

    public class UserProfileViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string DateOfBirth { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public decimal BodyFat { get; set; }

        public string ProfilePictureUrl { get; set; }

        public List<UserPostViewModel> Posts { get; set; }
    }
}
