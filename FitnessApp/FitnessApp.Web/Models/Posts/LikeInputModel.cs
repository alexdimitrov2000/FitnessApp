namespace FitnessApp.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class LikeInputModel
    {
        [Required]
        [StringLength(ValidationConstants.MAX_USER_NAME, MinimumLength = ValidationConstants.MIN_USER_NAME)]
        public string Username { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
