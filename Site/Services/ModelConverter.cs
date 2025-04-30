using LacunaDevOps.Api;
using LacunaDevOps.Api.Recipe;
using LacunaDevOps.Site.Entities;

namespace LacunaDevOps.Site.Services
{
    public class ModelConverter
    {
       public static IngredientModel ToModel(Ingredient value)
        {
            return new IngredientModel()
            {
                Id = value.Id,
                Name = value.Name,
                StandardUnitOfMeasure = value.StandardUnitOfMeasure,
            };
        }

        public static RecipeModel ToModel(Recipe value)
        {
            return new RecipeModel()
            {
                Id = value.Id,
                Name = value.Name,
                Preparation = value.Preparation,
                ingredientInRecipes = value.IngredientInRecipes.ConvertAll(ToModel),
            };
        }

        public static IngredientInRecipeModel ToModel(IngredientInRecipe value)
        {
            return new IngredientInRecipeModel()
            {
                Ingredient = value.Ingredient != null ? ToModel(value.Ingredient) : null,
                Quantity = value.Quantity,
            };
        }
    }
}
