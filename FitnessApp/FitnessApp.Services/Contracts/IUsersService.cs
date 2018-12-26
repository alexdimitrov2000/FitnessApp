namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models.Enums;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<IEnumerable<UsersListingServiceModel>> AllUsersAsync();

        Task<bool> ActivateUserAsync(string id);

        Task<bool> DeactivateUserAsync(string id);

        Task<bool> AddGoal(decimal weight, decimal height, int age, Gender gender, ActivityLevel activityLevel, WeightChangeType weightChangeType, string username);
                
    }
}
