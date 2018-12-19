namespace FitnessApp.Services.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<bool> AddCommentAsync(string content, string username, int postId);

        Task<bool> RemoveCommentAsync(int commentId, string username);
    }
}
