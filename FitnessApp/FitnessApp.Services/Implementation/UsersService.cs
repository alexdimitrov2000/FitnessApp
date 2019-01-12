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
    using FitnessApp.Models.Enums;
    using FitnessApp.Common.Constants;
    using FitnessApp.Models;

    public class UsersService : IUsersService
    {
        private readonly FitnessDbContext db;

        public UsersService(FitnessDbContext db)
        {
            this.db = db;
        }

        public async Task<FitnessUser> SetProfilePictureAsync(FitnessUser user, Image picture)
        {
            if (picture == null || user == null)
                return null;

            user.ProfilePicture = picture;

            this.db.Users.Update(user);
            await this.db.SaveChangesAsync();

            return user;
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

        public async Task<bool> AddGoal(decimal weight, decimal height, int age, Gender gender, ActivityLevel activityLevel, WeightChangeType weightChangeType, string username)
        {

            if(weight <= GlobalConstants.DEFAULT_GOAL_WEIGHT || height <= GlobalConstants.DEFAULT_GOAL_HEIGHT || age <= GlobalConstants.DEFAULT_GOAL_AGE)
            {
                return false;
            }

            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == username);
            
            if(username == null)
            {
                return false;
            }

            var calories = this.CalculateCaloriesForGoal(weight, height, age, gender, activityLevel, weightChangeType);
            var macronutrients = CalculateMacroNutrients(calories, weight, gender);
            var protein = macronutrients[0];
            var carbs = macronutrients[1];
            var fats = macronutrients[2];

            var goal = new Goal
            {
                Calories = calories,
                ActivityLevel = activityLevel,
                WeightChange = weightChangeType,
                Date = DateTime.UtcNow,
                Protein = protein,
                Carbohydrates = carbs,
                Fats = fats,
                User = user,
                UserId = user.Id
            };

            await this.db.Goals.AddAsync(goal);
            await this.db.SaveChangesAsync();

            return true;
            
        }

        private decimal[] CalculateMacroNutrients(decimal calories, decimal weight, Gender gender)
        {
            var protein = 2 * weight;
            calories -= (protein * 4);

            decimal fatsPercentage;

            if(gender == Gender.Female)
            {
                fatsPercentage = 0.45m;
            }
            else
            {
                fatsPercentage = 0.3m;
            }

            var fats = (fatsPercentage * calories) / 9;
            calories -= (fats * 9);

            var carbs = calories / 4;

            return new decimal[3] { protein, carbs, fats };
        }

        private decimal CalculateCaloriesForGoal(decimal weight, decimal height, int age, Gender gender, ActivityLevel activityLevel, WeightChangeType weightChangeType)
        {
            var activityMultiplier = this.GetMultiplier(activityLevel);

            var genderMultipliers = GetGenderMultipliers(gender);
            var genderWeightMultiplier = genderMultipliers[0];
            var genderHeightMultiplier = genderMultipliers[1];
            var genderAgeMultiplier = genderMultipliers[2];
            var genderMultiplier = genderMultipliers[3];

            var weightChangeCalories = GetWeightChangeCalories(weightChangeType);

            var dailyCalories = genderMultiplier + (weight * genderWeightMultiplier) + (height * genderHeightMultiplier) - (age * genderAgeMultiplier); 

            var calories = (activityMultiplier * dailyCalories) + weightChangeCalories;

            return calories;
            
        }

        private int GetWeightChangeCalories(WeightChangeType weightChangeType)
        {
            switch (weightChangeType)
            {
                case WeightChangeType.Maintaing:
                    return GlobalConstants.MAINTAIN_CALORIES;
                case WeightChangeType.Bulk:
                    return GlobalConstants.BULKING_CALORIES;
                case WeightChangeType.Cut:
                    return GlobalConstants.CUTTING_CALORIES;
                default:
                    break;
            }
            return 0;
        }

        private decimal GetMultiplier(ActivityLevel activityLevel)
        {
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    return GlobalConstants.SEDENTARY_MULTIPLIER;
                case ActivityLevel.LightlyActive:
                    return GlobalConstants.LIGHTLYACTIVE_MUTLIPLIER;
                case ActivityLevel.Active:
                    return GlobalConstants.ACTIVE_MUTLIPLIER;
                case ActivityLevel.VeryActive:
                    return GlobalConstants.VERYACTIVE_MULTIPLIER;
                default:
                    break;
            }

            return 0;
        }
        private decimal[] GetGenderMultipliers(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return new decimal[4] { GlobalConstants.MALE_WEIGHT_MULTIPLIER, GlobalConstants.MALE_HEIGHT_MULTIPLIER, GlobalConstants.MALE_AGE_MULTIPLIER, GlobalConstants.MALE_MULTIPLIER };
                case Gender.Female:
                    return new decimal[4] { GlobalConstants.FEMALE_WEIGHT_MULTIPLIER, GlobalConstants.FEMALE_HEIGHT_MULTIPLIER, GlobalConstants.FEMALE_AGE_MULTIPLIER, GlobalConstants.FEMALE_MULTIPLIER };
                default:
                    break;
            }

            return new decimal[4];
        }
    }
}
