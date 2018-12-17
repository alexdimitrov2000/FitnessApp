namespace FitnessApp.Data
{
    using FitnessApp.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class FitnessDbContext : IdentityDbContext<FitnessUser, IdentityRole, string>
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
            : base(options)
        {
        }
    }
}
