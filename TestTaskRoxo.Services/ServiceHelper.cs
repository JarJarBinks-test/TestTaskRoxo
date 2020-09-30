using Microsoft.Extensions.DependencyInjection;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Core;

namespace TestTaskRoxo.Services
{
    public static class ServiceHelper
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IOrderDetailDataService, OrderDetailDataService>();
            services.AddScoped<IStatusDataService, StatusDataService>();
            services.AddScoped<IStructureDataService, StructureDataService>();
            services.AddDalServices();
        }
    }
}
