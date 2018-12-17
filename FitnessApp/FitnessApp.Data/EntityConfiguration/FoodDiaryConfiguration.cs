namespace FitnessApp.Data.EntityConfiguration
{
    using FitnessApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FoodDiaryConfiguration : IEntityTypeConfiguration<FoodDiary>
    {
        public void Configure(EntityTypeBuilder<FoodDiary> builder)
        {
            builder.HasKey(fd => fd.Id);

            builder.HasOne(fd => fd.User)
                .WithMany(u => u.FoodDiaries)
                .HasForeignKey(fd => fd.UserId);
        }
    }
}
