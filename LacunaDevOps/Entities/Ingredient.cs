using LacunaDevOps.Api;

namespace LacunaDevOps.Site.Entities
{
    public class Ingredient
    {        
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public UnitOfMeasure StandardUnitOfMeasure { get; set; }

        public bool IsDeleted { get; set; }

        public List<IngredientInRecipe> IngredientInRecipes { get; set; } = [];
    }
}
