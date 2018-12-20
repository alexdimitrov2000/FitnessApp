namespace FitnessApp.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using FitnessApp.Models;
    using FitnessApp.Services.Models.Foods;
    using Microsoft.EntityFrameworkCore;

    public class FoodsService : IFoodsService
    {
        private readonly FitnessDbContext db;

        public FoodsService(FitnessDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<FoodsListingModel>> AllFoodForUser(string username)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var foods = user.MyFoods.Select(f => new FoodsListingModel
            {
                Name = f.Food.Name,
                Protein = f.Food.Protein,
                Carbohydrates = f.Food.Carbohydrates,
                Fats = f.Food.Fats
            }).ToList();

            return foods;
        }

        public async Task<bool> CreateFood(string username, string name, decimal protein, decimal carbohydrates, decimal fats)
        {

            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return false;
            }

            if (name.Length <= 0 || protein < 0 || carbohydrates < 0 || fats < 0)
            {
                return false;
            }

            var food = new Food
            {
                Name = name,
                Protein = protein,
                Carbohydrates = carbohydrates,
                Fats = fats
            };

            await this.db.Foods.AddAsync(food);
            await this.db.SaveChangesAsync();

            var userFood = new UserFood
            {
                FoodId = food.Id,
                Food = food,
                UserId = user.Id,
                User = user
            };

            await this.db.UsersFoods.AddAsync(userFood);

            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
