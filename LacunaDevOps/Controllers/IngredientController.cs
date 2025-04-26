using LacunaDevOps.Api;
using LacunaDevOps.Site.Entities;
using LacunaDevOps.Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LacunaDevOps.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public IngredientController(
               AppDbContext appDbContext
            ) 
        { 
            this.appDbContext = appDbContext;  
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<IngredientModel>> Get()
        {
            var ingredients = await appDbContext.Ingredients.ToListAsync();
            return ingredients.ConvertAll(ModelConverter.ToModel);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IngredientModel> Get(Guid id)
        {
            var ingredient = await appDbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Ingrediente com o id: {id} não encontrado");
            return ModelConverter.ToModel(ingredient);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IngredientModel> Post([FromBody] IngredientData value)
        {
            var ingredient = new Ingredient()
            {
                Name = value.Name,
                StandardUnitOfMeasure = value.StandardUnitOfMeasure,
            };

            appDbContext.Ingredients.Add(ingredient);
            await appDbContext.SaveChangesAsync();

            return ModelConverter.ToModel(ingredient);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IngredientModel> Put(Guid id, [FromBody] IngredientData value)
        {
            var ingredient = await appDbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Ingrediente com o id: {id} não encontrado");
            ingredient.StandardUnitOfMeasure = value.StandardUnitOfMeasure;
            ingredient.Name = value.Name;
            await appDbContext.SaveChangesAsync();
            return ModelConverter.ToModel(ingredient);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IngredientModel> DeleteAsync(Guid id)
        {
            var ingredient = await appDbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Ingrediente com o id: {id} não encontrado");
            ingredient.IsDeleted = true;
            await appDbContext.SaveChangesAsync();
            return ModelConverter.ToModel(ingredient);
        }
    }
}
