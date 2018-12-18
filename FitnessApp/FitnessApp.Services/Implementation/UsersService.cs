namespace FitnessApp.Services.Implementation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Contracts;
    using Data;
    using Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class UsersService : IUsersService
    {
        private readonly FitnessDbContext db;

        public UsersService(FitnessDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DeactivateUserAsync(string id)
        {
            var user = await this.db.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateUserAsync(string id)
        {
            var user = await this.db.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            user.IsActive = true;
            
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UsersListingServiceModel>> AllUsersAsync()
        {
            var users = await this.db.Users.Select(u => new UsersListingServiceModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive

            }).ToListAsync();

            if(users == null)
            {
                throw new InvalidOperationException("No users found!");
            }

            return users;
        }
    }
}
