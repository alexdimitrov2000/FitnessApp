namespace FitnessApp.Data
{
    using FitnessApp.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using FitnessApp.Data.EntityConfiguration;

    public class FitnessDbContext : IdentityDbContext<FitnessUser, IdentityRole, string>
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodDiary> FoodDiaries { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<UserFood> UsersFoods { get; set; }

        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new FoodDiaryConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new ReplyConfiguration());
            builder.ApplyConfiguration(new UserFoodConfiguration());
            builder.ApplyConfiguration(new GoalConfiguration());
        }
    }
}
