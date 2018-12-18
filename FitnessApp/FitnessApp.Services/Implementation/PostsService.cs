namespace FitnessApp.Services.Implementation
{
    using Data;
    using Contracts;
    using FitnessApp.Models;

    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;

    public class PostsService : IPostsService
    {
        private readonly FitnessDbContext context;

        public PostsService(FitnessDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(string title, string content, Image image, int categoryId, string username)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || categoryId <= 0 || string.IsNullOrEmpty(username) || image == null)
                return false;

            var userId = await this.context.Users.Where(u => u.UserName == username).Select(u => u.Id).FirstOrDefaultAsync();
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
    }
}
