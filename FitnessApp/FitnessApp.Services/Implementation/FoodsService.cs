namespace FitnessApp.Services.Implementation
{
    using System;
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

        public async Task<IEnumerable<FoodsListingModel>> AllFoodForUserAsync(string username)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var foods = user.MyFoods.Select(f => new FoodsListingModel
            {
                Id = f.FoodId,
                Name = f.Food.Name,
                Protein = f.Food.Protein,
                Carbohydrates = f.Food.Carbohydrates,
                Fats = f.Food.Fats
            }).ToList();

            return foods;
        }

        public async Task<IEnumerable<FoodsListingModel>> FindFoodsAsync(string searchText)
        {
            var food = await this.db.Foods.Where(f => f.Name.Contains(searchText)).Select(f => new FoodsListingModel
            {
                Id = f.Id,
                Name = f.Name,
                Protein = f.Protein,
                Carbohydrates = f.Carbohydrates,
                Fats = f.Fats
            }).ToListAsync();

            return food;
        }

        public async Task<bool> CreateFoodAsync(string username, string name, decimal protein, decimal carbohydrates, decimal fats)
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
        
        public async Task<FoodsListingModel> GetFoodInfoAsync(int id)
        {
            var food = await this.db.Foods.Where(f => f.Id == id).Select(f => new FoodsListingModel
            {
                Name = f.Name,
                Protein = f.Protein,
                Carbohydrates = f.Carbohydrates,
                Fats = f.Fats
            }).FirstOrDefaultAsync();

            if(food == null)
            {
                throw new InvalidOperationException($"No food with id: {id} found.");
            }

            return food;
        }

        public async Task<bool> IsDiaryCreatedAsync(DateTime date, string username)
        {
            return await this.db.FoodDiaries.AnyAsync(fd => fd.Date.Date.CompareTo(date.Date) == 0 && fd.User.UserName == username);
        }

        public async Task<bool> CreateDiaryAsync(DateTime date, string username)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return false;
            }

            var foodDiary = new FoodDiary
            {
                Date = date,
                UserId = user.Id,
                User = user
            };

            await this.db.FoodDiaries.AddAsync(foodDiary);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<FoodDiaryListingModel> FindDiaryAsync(DateTime date, string username)
        {
            var diary = await this.db.FoodDiaries.Include(f => f.Meals).Select(fd => new FoodDiaryListingModel
            {
                Id = fd.Id,
                Date = fd.Date,
                Meals = fd.Meals,
                User = fd.User,
                UserId = fd.User.Id
            }).FirstOrDefaultAsync(fd => fd.Date.CompareTo(date.Date) == 0 && fd.User.UserName == username);

            var diaryFood = await this.db.DiaryFoods.Where(df => df.FoodDiaryId == diary.Id).ToListAsync();

            diary.Meals = diaryFood;
            
            if(diary == null)
            {
                throw new InvalidOperationException($"No food diary for date: {date.Date} found!");
            }
            
            return diary;
        }

        public async Task<bool> AddToDiaryAsync(DateTime date, int foodId, decimal multiplier, string username)
        {
            if (multiplier <= 0)
            {
                return false;
            }

            var food = await this.db.Foods.FirstOrDefaultAsync(f => f.Id == foodId);

            if (food == null)
            {
                return false;
            }
            
            var diary = await this.db.FoodDiaries
                .FirstOrDefaultAsync(fd => fd.Date.Date.CompareTo(date.Date) == 0 && fd.User.UserName == username);

            if (diary == null)
            {
                return false;
            }

            var diaryfood = await this.db.DiaryFoods
                .FirstOrDefaultAsync(df => df.FoodId == foodId && df.Multiplier == multiplier && df.FoodDiaryId == diary.Id);

            if(diaryfood == null)
            {
                diaryfood = new DiaryFood
                {
                    Food = food,
                    FoodId = foodId,
                    Multiplier = multiplier,
                    FoodDiary = diary,
                    FoodDiaryId = diary.Id
                };

                await this.db.DiaryFoods.AddAsync(diaryfood);
            }
            else
            {
                diaryfood.Multiplier += multiplier;
            }

            diary.Meals.Add(diaryfood);
                                    
            await this.db.SaveChangesAsync();
            
            return true;
        }
                
    }
}
