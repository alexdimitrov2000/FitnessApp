namespace FitnessApp.Services.Implementation
{
    using Data;
    using Contracts;
    using FitnessApp.Models;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class PostsService : IPostsService
    {
        private readonly FitnessDbContext context;

        public PostsService(FitnessDbContext context)
        {
            this.context = context;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var post = await this.context.Posts
                .FirstOrDefaultAsync(p => p.Id == id);

            return post;
        }

        public async Task<bool> AddLikeAsync(string username, int postId)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == username);

            if(user == null)
            {
                return false;
            }

            var post = await this.context.Posts.FindAsync(postId);

            if(post == null)
            {
                return false;
            }

            var postLike = await this.context.Likes.FirstOrDefaultAsync(l => l.UserId == user.Id && l.PostId == postId);

            if(postLike != null)
            {
                return false;
            }

            postLike = new Like
            {
                User = user,
                UserId = user.Id,
                PostId = post.Id,
                Post = post
            };

            await this.context.Likes.AddAsync(postLike);
            await this.context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> CreateAsync(string title, string content, Image image, int categoryId, string username)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || categoryId <= 0 || string.IsNullOrEmpty(username) || image == null)
                return false;

            var userId = await this.context.Users.Where(u => u.Name == username).Select(u => u.Id).FirstOrDefaultAsync();
            var category = await this.context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (userId == null || category == null)
                return false;

            var post = new Post
            {
                Title = title,
                Content = content,
                Category = category,
                Image = image,
                UserId = userId
            };

            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveLikeAsync(string username, int postId)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == username);

            if (user == null)
            {
                return false;
            }

            var post = await this.context.Posts.FindAsync(postId);

            if (post == null)
            {
                return false;
            }

            var postLike = await this.context.Likes.FirstOrDefaultAsync(l => l.UserId == user.Id && l.PostId == postId);

            if(postLike == null)
            {
                return false;
            }

            post.Likes.Remove(postLike);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsLikedAsync(string username, int postId)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == username);

            if(user == null)
            {
                return false;
            }

            var post = await this.context.Posts.FindAsync(postId);

            if(post == null)
            {
                return false;
            }

            return await this.context.Likes.AnyAsync(l => l.UserId == user.Id && l.PostId == postId);
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await this.context.Posts.ToListAsync();
        }
    }
}
