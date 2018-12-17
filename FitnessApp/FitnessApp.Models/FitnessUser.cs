namespace FitnessApp.Models
{
    using Microsoft.AspNetCore.Identity;

    using System;

    public class FitnessUser : IdentityUser
    {
        public string Name { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? BodyFat { get; set; }

        public Image ProfilePicture { get; set; }
    }
}
