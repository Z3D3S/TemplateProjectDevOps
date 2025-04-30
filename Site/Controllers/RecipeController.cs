using LacunaDevOps.Api;
using LacunaDevOps.Api.Recipe;
using LacunaDevOps.Site.Entities;
using LacunaDevOps.Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LacunaDevOps.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly AppDbContext appDbContext;
        public RecipeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<RecipeModel>> Get()
        {
            var recipes = await appDbContext.Recipes
                .Include(r => r.IngredientInRecipes)
                .ThenInclude(ir => ir.Ingredient)
                .ToListAsync();

            return recipes.ConvertAll(ModelConverter.ToModel);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<RecipeModel> Get(Guid id)
        {
            var recipe = await appDbContext.Recipes.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Receita com o id: {id} não encontrado");
            return ModelConverter.ToModel(recipe);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<RecipeModel> Post([FromBody] RecipeData value)
        {
            var recipe = new Recipe()
            {
                Name = value.Name,
                Preparation = value.Preparation,
                IngredientInRecipes = value.IngredientInRecipeDatas.ConvertAll(ir => new IngredientInRecipe() { 
                    IngredientId = ir.IngredientId,
                    Quantity = ir.Quantity
                }),
            };

            appDbContext.Recipes.Add(recipe);
            await appDbContext.SaveChangesAsync();

            return ModelConverter.ToModel(recipe);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<RecipeModel> Put(Guid id, [FromBody] RecipeData value)
        {
            var recipe = await appDbContext.Recipes
                .Include(r => r.IngredientInRecipes)
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Receita com o id: {id} não encontrado");
            
            recipe.Name = value.Name;
            recipe.Preparation = value.Preparation;
            recipe.IngredientInRecipes = value.IngredientInRecipeDatas.ConvertAll(ir => new IngredientInRecipe()
            {
                IngredientId = ir.IngredientId,
                Quantity = ir.Quantity
            });

            await appDbContext.SaveChangesAsync();
            
            return ModelConverter.ToModel(recipe);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<RecipeModel> DeleteAsync(Guid id)
        {
            var recipe = await appDbContext.Recipes.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Receita com o id: {id} não encontrado");
            recipe.IsDeleted = true;
            await appDbContext.SaveChangesAsync();
            return ModelConverter.ToModel(recipe);
        }

    }
}
