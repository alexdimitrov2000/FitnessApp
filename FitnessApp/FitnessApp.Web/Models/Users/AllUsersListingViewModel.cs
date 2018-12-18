namespace FitnessApp.Web.Models.Users
{
    using System.Collections.Generic;

    public class AllUsersListingViewModel
    {
        public IEnumerable<UsersListingViewModel> AllUsers { get; set; }
    }
}
