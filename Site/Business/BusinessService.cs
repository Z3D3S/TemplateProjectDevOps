using Microsoft.EntityFrameworkCore;
using System;

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
