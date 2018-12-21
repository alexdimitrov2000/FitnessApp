namespace FitnessApp.Web.Models.Foods
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Foods;

    public class SearchFoodViewModel
    {
        public IEnumerable<FoodsListingModel> All { get; set; }
        
        [MinLength(1)]
        public string SearchText { get; set; }
    }
}
