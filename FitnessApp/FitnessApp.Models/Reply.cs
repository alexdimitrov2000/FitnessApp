﻿namespace FitnessApp.Models
{
    using Common.Constants;

    using System.ComponentModel.DataAnnotations;

    public class Reply
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MIN_REPLY_CONTENT)]
        public string Content { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public string UserId { get; set; }
        public virtual FitnessUser User { get; set; }
    }
}
