namespace FitnessApp.Services.Contracts
{
    using Models.Foods;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFoodsService
    {
        Task<bool> CreateFood(string username, string name, decimal protein, decimal carbohydrates, decimal fats);

        Task<IEnumerable<FoodsListingModel>> AllFoodForUser(string username);
    }
}
