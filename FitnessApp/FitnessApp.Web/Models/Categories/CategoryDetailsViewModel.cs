using FitnessApp.Models;
using System.Collections.Generic;

namespace FitnessApp.Web.Models.Categories
{
    public class CategoryDetailsViewModel
    {
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
