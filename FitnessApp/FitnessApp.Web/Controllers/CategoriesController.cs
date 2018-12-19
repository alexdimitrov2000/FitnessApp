namespace FitnessApp.Web.Controllers
{
    using Models.Categories;
    using Services.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;
    using System.Linq;

    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var categoryName = model.Name;

            await this.categoriesService.CreateAsync(categoryName);

            return this.RedirectToAction("Details", "Categories", new { name = categoryName });
        }

        [Route(nameof(Details) + "/{name}")]
        public async Task<IActionResult> Details(string name)
        {
            var category = await this.categoriesService.GetByNameAsync(name);

            if (category == null)
                return this.NotFound();

            var categoryDetailsViewModel = new CategoryDetailsViewModel
            {
                Name = category.Name,
                Posts = category.Posts.ToList()
            };

            return this.View(categoryDetailsViewModel);
        }
    }
}
