using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RecipeService
{
    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        RecipeResult GetRecipes();

        [OperationContract]
        RecipeResult GetRecipeById(int id);

        [OperationContract]
        RecipeResult SearchForRecipeByName(String name);

        [OperationContract]
        RecipeResult GetIngredientsByRecipeId(int id);

        [OperationContract]
        RecipeResult GetDirectionsByRecipeId(int id);
    }

}
