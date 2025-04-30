using LacunaDevOps.Site.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LacunaDevOps.Site.Business
{
    public partial class BusinessService
    {
        private readonly AppDbContext dbContext;
        public BusinessService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Recipe> GetRequireRecipe(Guid id)
        {
            await dbContext.Recipes.FirstOrDefaultAsync(i => i.Id == id) ?? throw new Exception($"Receita com o id: {id} não encontrado");

        }
    }
}
