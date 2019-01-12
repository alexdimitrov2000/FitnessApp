namespace FitnessApp.Services.Contracts
{
    using Models;
    using FitnessApp.Models;
    using FitnessApp.Models.Enums;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IUsersService
    {
        Task<IEnumerable<UsersListingServiceModel>> AllUsersAsync();

        Task<bool> ActivateUserAsync(string id);

        Task<bool> DeactivateUserAsync(string id);

        Task<bool> AddGoal(decimal weight, decimal height, int age, Gender gender, ActivityLevel activityLevel, WeightChangeType weightChangeType, string username);

        Task<FitnessUser> SetProfilePictureAsync(FitnessUser user, Image picture);                
    }
}
