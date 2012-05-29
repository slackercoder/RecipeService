using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataLayer;

namespace RecipeService
{
    public class RecipeService : IRecipeService
    {
        public RecipeResult GetRecipes()
        {
            RecipeResult ret = new RecipeResult();

            using (db505e4eb8d47b40b7a6d7a05a001ba2a9Entities data = new db505e4eb8d47b40b7a6d7a05a001ba2a9Entities())
            {
                ret.RecipeNames = data.Recipes.ToList();
            }

            return ret;
        }
    }
}
