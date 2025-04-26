namespace LacunaDevOps.Site.Entities
{
    public class IngredientInRecipe
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
    }
}
