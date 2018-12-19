namespace FitnessApp.Models
{
    using Common.Constants;

    using Microsoft.AspNetCore.Identity;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FitnessUser : IdentityUser
    {
        public FitnessUser()
        {
            this.Likes = new HashSet<Like>();
            this.MyFoods = new List<UserFood>();
            this.FoodDiaries = new List<FoodDiary>();
            this.Comments = new List<Comment>();
            this.Replies = new List<Reply>();
            this.Posts = new List<Post>();
        }
        
        [StringLength(ValidationConstants.MAX_USER_NAME, MinimumLength = ValidationConstants.MIN_USER_NAME)]
        public string Name { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? BodyFat { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Image ProfilePicture { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<UserFood> MyFoods { get; set; }

        public virtual ICollection<FoodDiary> FoodDiaries { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
