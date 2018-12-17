namespace FitnessApp.Data.EntityConfiguration
{
    using FitnessApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFoodConfiguration : IEntityTypeConfiguration<UserFood>
    {
        public void Configure(EntityTypeBuilder<UserFood> builder)
        {
            builder.HasKey(uf => new { uf.FoodId, uf.UserId });

            builder.HasOne(uf => uf.Food)
                .WithMany(f => f.Users)
                .HasForeignKey(uf => uf.FoodId);

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.MyFoods)
                .HasForeignKey(uf => uf.UserId);
        }
    }
}
