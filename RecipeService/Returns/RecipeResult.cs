using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace RecipeService
{
    public class RecipeResult
    {
        public List<Recipe> RecipeNames { get; set; }
        public List<IngredientList> Ingredients { get; set; }
        public List<Direction> Directions { get; set; }
    }
}
