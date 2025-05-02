using Microsoft.EntityFrameworkCore;
using System;
using {{TemplateName}}.Site.Entities;

namespace {{TemplateName}}.Site.Business
{
    public partial class BusinessService
    {
        private readonly AppDbContext dbContext;
        public BusinessService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
