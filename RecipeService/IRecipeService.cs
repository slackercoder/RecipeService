using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RecipeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
        RecipeResult AddRecipe(); //Needs to be updated.
    }

}
