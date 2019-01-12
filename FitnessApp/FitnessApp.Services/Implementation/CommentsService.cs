namespace FitnessApp.Services.Implementation
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using FitnessApp.Models;

    public class CommentsService : ICommentsService
    {
        private readonly FitnessDbContext db;

        public CommentsService(FitnessDbContext db)
        {
            this.db = db;
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
