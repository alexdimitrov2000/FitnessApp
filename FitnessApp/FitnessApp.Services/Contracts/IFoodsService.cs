namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;
    using Models.Foods;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFoodsService
    {
        Task<bool> CreateFoodAsync(string username, string name, decimal protein, decimal carbohydrates, decimal fats);

        Task<bool> IsDiaryCreatedAsync(DateTime date, string username);

        Task<bool> CreateDiaryAsync(DateTime date, string username);

        Task<bool> AddToDiaryAsync(DateTime date, int foodId, int multiplier, string username);

        Task<FoodDiaryListingModel> FindDiaryAsync(DateTime date, string usernmae);

        Task<IEnumerable<FoodsListingModel>> AllFoodForUserAsync(string username);

        Task<IEnumerable<FoodsListingModel>> FindFoodsAsync(string searchText);

        Task<FoodsListingModel> GetFoodInfoAsync(int id);
    }
}
