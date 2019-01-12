namespace FitnessApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Foods;
    using FitnessApp.Services.Contracts;
    using System;
    using System.Globalization;
    using System.Linq;

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
        
        public async Task<IActionResult> Diary(string date, string username)
        {
            DateTime dateTime;
            
            if (date == null)
            {
                dateTime = DateTime.UtcNow.Date;
            }
            else
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy", null, DateTimeStyles.None, out dateTime);
            }
            

            var exists = await this.foods.IsDiaryCreatedAsync(dateTime, username);
            
            if (!exists)
            {
                var success = await this.foods.CreateDiaryAsync(dateTime, username);

                if (!success)
                    return BadRequest();
            }

            var diary = await this.foods.FindDiaryAsync(dateTime, username);

            var diaryModel = new FoodDiaryViewModel
            {
                Id = diary.Id,
                EatenCalories = diary.EatenCalories,
                Date = diary.Date,
                Meals = diary.Meals,
                User = diary.User,
                UserId = diary.User.Id,
                Goal = diary.User.Goals.LastOrDefault()
            };

            return View(diaryModel);
        
        }

        public async Task<IActionResult> AddToDiary(string date, string username)
        {
            var food = await this.foods.AllFoodForUserAsync(username);
            
            return View(new AddToDiaryViewModel { All = food, Date = date });
        }

        [HttpPost]
        public async Task<IActionResult> AddToDiary(string username, AddToDiaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            DateTime dateTime;
            DateTime.TryParseExact(model.Date, "dd-MM-yyyy", null, DateTimeStyles.None, out dateTime);


            var success = await this.foods.AddToDiaryAsync(dateTime, model.FoodId, model.Multiplier, username);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Diary), new { date = model.Date, username = User.Identity.Name });
        }
        

        [HttpPost]
        public async Task<IActionResult> AddFood(AddFoodInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await this.foods.CreateFoodAsync(User.Identity.Name, model.Name, model.Protein, model.Carbohydrates, model.Fats);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(MyFoods));
        }
    }
}
