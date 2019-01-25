namespace FitnessApp.Services.Implementation
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using FitnessApp.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Services.Models.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly FitnessDbContext db;
        private readonly ICloudinaryService cloudinary;

        public CommentsService(FitnessDbContext db, ICloudinaryService cloudinary)
        {
            this.db = db;
            this.cloudinary = cloudinary;
        }

        public async Task<bool> AddCommentAsync(string content, string username, int postId)
        {
            if(content.Length <= 0 || string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return false;
            }

            var post = await this.db.Posts.FindAsync(postId);

            if(post == null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                User = user,
                UserId = user.Id,
                Post = post,
                PostId = post.Id
            };

            await this.db.Comments.AddAsync(comment);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CommentsListingModel>> LoadCommentsAsync(int pageSize, int currentPage, int postId)
        {
            if(pageSize <= 0 || currentPage <= 0)
            {
                throw new InvalidOperationException("Invalid page data!");
            }
            var comments = await this.db.Comments.Where(c => c.PostId == postId)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CommentsListingModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    ProfilePictureUrl = this.cloudinary.BuildPictureUrl(c.User.ProfilePicture)
                }).ToListAsync();

            if(comments == null)
            {
                throw new InvalidOperationException($"No post with id: {postId} found!");
            }

            return comments;
        }

        public async Task<bool> RemoveCommentAsync(int commentId, string username)
        {
            var comment = await this.db.Comments.FindAsync(commentId);
            
            if(comment == null)
            {
                return false;
            }

            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return false;
            }
            if(user.UserName != "admin")
            {
                if (comment.UserId != user.Id)
                {
                    return false;
                }
            }
            
            this.db.Comments.Remove(comment);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
