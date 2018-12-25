namespace FitnessApp.Web.Models.Foods
{
    using Services.Models.Foods;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddToDiaryViewModel
    {
        public IEnumerable<FoodsListingModel> All { get; set; }

        public string Date { get; set; }

        [Required]
        public int FoodId { get; set; }

        [Required]
        public int Multiplier { get; set; }
    }
}
