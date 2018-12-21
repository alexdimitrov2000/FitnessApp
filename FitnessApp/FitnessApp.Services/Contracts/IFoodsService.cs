namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;
    using Models.Foods;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFoodsService
    {
        Task<bool> CreateFood(string username, string name, decimal protein, decimal carbohydrates, decimal fats);

        Task<IEnumerable<FoodsListingModel>> AllFoodForUserAsync(string username);

        Task<IEnumerable<FoodsListingModel>> FindFoodsAsync(string searchText);

        Task<FoodsListingModel> GetFoodInfoAsync(int id);
    }
}
