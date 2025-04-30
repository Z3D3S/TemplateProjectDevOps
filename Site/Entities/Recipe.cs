namespace LacunaDevOps.Site.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Preparation { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public List<IngredientInRecipe> IngredientInRecipes { get; set; } = [];
    }
}
