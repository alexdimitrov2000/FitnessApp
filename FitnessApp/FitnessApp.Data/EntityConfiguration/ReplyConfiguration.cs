namespace FitnessApp.Data.EntityConfiguration
{
    using FitnessApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasOne(r => r.Comment)
                .WithMany(c => c.Replies)
                .HasForeignKey(r => r.CommentId);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Replies)
                .HasForeignKey(r => r.UserId);
        }
    }
}
