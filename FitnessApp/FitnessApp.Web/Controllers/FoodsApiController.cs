namespace FitnessApp.Web.Controllers
{
    using FitnessApp.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/foods")]
    [ApiController]
    public class FoodsApiController : Controller
    {
        private readonly IFoodsService foods;

        public FoodsApiController(IFoodsService foods)
        {
            this.foods = foods;
        }

        [HttpGet]
        [Route(nameof(UpdateInfo) + "/{id}")]
        public async Task<IActionResult> UpdateInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var info = await this.foods.GetFoodInfoAsync(id);

            return new JsonResult(info);
        }
    }
}
