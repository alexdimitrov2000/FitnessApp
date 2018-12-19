namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<bool> CreateAsync(string name);

        Task<Category> GetByNameAsync(string name);

        Task<List<Category>> GetAllAsync();
    }
}
