namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;
    using Services.Models.Comments;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<bool> AddCommentAsync(string content, string username, int postId);

        Task<bool> RemoveCommentAsync(int commentId, string username);

        Task<IEnumerable<CommentsListingModel>> LoadCommentsAsync(int pageSize, int currentPage, int postId);
    }
}
