namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;

    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<bool> CreateAsync(string title, string content, Image image, int categoryId, string username);

        Task<bool> AddLikeAsync(string username, int postId);

        Task<bool> RemoveLikeAsync(string username, int postId);

        Task<bool> IsLikedAsync(string username, int postId);
    }
}
