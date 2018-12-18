namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;

    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<bool> CreateAsync(string title, string content, Image image, int categoryId, string username);
    }
}
