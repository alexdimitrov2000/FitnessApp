namespace FitnessApp.Services.Implementation
{
    using Contracts;
    using Data;
    using FitnessApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly FitnessDbContext context;

        public CategoriesService(FitnessDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var category = new Category
            {
                Name = name
            };

            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var category = await this.context.Categories.FirstOrDefaultAsync(c => c.Name == name);

            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await this.context.Categories.ToListAsync();
        }
    }
}
