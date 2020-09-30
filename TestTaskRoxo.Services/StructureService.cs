using Microsoft.Extensions.Logging;
using System;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Services
{
    public class StructureService: IStructureService
    {
        readonly ILogger<IStructureService> logger;
        readonly IStructureDataService structureDataService;
        public StructureService(IStructureDataService structureDataService, ILogger<IStructureService> logger)
        {
            this.structureDataService = structureDataService ?? throw new ArgumentNullException(nameof(structureDataService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void EnsureStructureCreated()
        {
            logger.LogInformation($"{nameof(EnsureStructureCreated)}.");
            structureDataService.EnsureStructureCreated();
        }
    }
}
