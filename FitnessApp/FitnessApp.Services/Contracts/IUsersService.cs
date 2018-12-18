namespace FitnessApp.Services.Contracts
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<IEnumerable<UsersListingServiceModel>> AllUsersAsync();

        Task<bool> ActivateUserAsync(string id);

        Task<bool> DeactivateUserAsync(string id);
        
    }
}
