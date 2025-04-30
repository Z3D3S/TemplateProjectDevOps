using Microsoft.EntityFrameworkCore;

namespace LacunaDevOps.Site.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ← BOA PRÁTICA!

            modelBuilder.Entity<Ingredient>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<Recipe>().HasQueryFilter(r => !r.IsDeleted);

            modelBuilder.Entity<IngredientInRecipe>()
            .HasKey(ir => new { ir.RecipeId, ir.IngredientId }); // chave composta

            modelBuilder.Entity<IngredientInRecipe>()
                .HasOne(ir => ir.Recipe)
                .WithMany(r => r.IngredientInRecipes)
                .HasForeignKey(ir => ir.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngredientInRecipe>()
                .HasOne(ir => ir.Ingredient)
                .WithMany(i => i.IngredientInRecipes)
                .HasForeignKey(ir => ir.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<IngredientInRecipe> IngredientInRecipes { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
