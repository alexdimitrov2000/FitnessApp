namespace FitnessApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Foods;
    using FitnessApp.Services.Contracts;


    public class FoodsController : Controller
    {
        private readonly IFoodsService foods;

        public FoodsController(IFoodsService foods)
        {
            this.foods = foods;
        }

        public async Task<IActionResult> MyFoods()
        {
            var food = await this.foods.AllFoodForUserAsync(User.Identity.Name);

            return View(food);
        }

        public async Task<IActionResult> AddFood()
        {
            return View(new AddFoodInputModel());
        }

        public async Task<IActionResult> All(SearchFoodViewModel model)
        {
            var food = await this.foods.FindFoodsAsync(model.SearchText);

            return View(new SearchFoodViewModel { All = food });
        }
        

        [HttpPost]
        public async Task<IActionResult> AddFood(AddFoodInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await this.foods.CreateFood(User.Identity.Name, model.Name, model.Protein, model.Carbohydrates, model.Fats);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(MyFoods));
        }
    }
}
