using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using TestTaskRoxo.BaseClasses.Interaces;

namespace TestTaskRoxo.Core
{
    public class StructureDataService : IStructureDataService
    {
        ITestTaskDbContext context;
        ILogger<IStructureDataService> logger;

        public StructureDataService(ITestTaskDbContext dbContext, ILogger<IStructureDataService> logger)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void EnsureStructureCreated()
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during creating the DB.");
                throw;
            }
        }
    }
}
