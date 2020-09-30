using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.Core.Tables;

namespace TestTaskRoxo.Core
{
    public static class DalHelper
    {
        static Lazy<Mapper> autoMapper = new Lazy<Mapper>(() => {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DbOrder, Order>().ReverseMap();
                cfg.CreateMap<DbOrderDetail, OrderDetail>().ReverseMap();
                cfg.CreateMap<DbProduct, Product>().ReverseMap();
                cfg.CreateMap<DbOrderStatus, OrderStatus>().ReverseMap();
            });
            return new Mapper(config);
        }, true);

        public static async Task<R> DoWithResult<T, R>(this ITestTaskDbContext context, Func<DbSet<T>, Task<R>> act, Boolean shouldSave = false)
            where T : class
        {
            var res = await act(context.Set<T>());
            if (!shouldSave)
                return res;

            await context.SaveChangesAsync();
            return res;
        }

        public static async Task Do<T>(this ITestTaskDbContext context, Func<DbSet<T>, Task> act, Boolean shouldSave = false)
            where T : class
        {
            await act(context.Set<T>());
            if (!shouldSave)
                return;

            await context.SaveChangesAsync();
            return;
        }

        public static void Do<T>(this ITestTaskDbContext context, Action<DbSet<T>> act, Boolean shouldSave = false)
            where T : class
        {
            act(context.Set<T>());
            if (!shouldSave)
                return;

            context.SaveChanges();
            return;
        }

        public static T To<T>(this Object from) => autoMapper.Value.Map<T>(from);

        public static void AddDalServices(this IServiceCollection services)
        {
            services.AddTransient<ITestTaskDbContext, TestTaskRoxoDbContext>(x => {
                var ob = new DbContextOptionsBuilder<TestTaskRoxoDbContext>();
                ob.UseSqlServer(x.GetService<IConfiguration>().GetConnectionString("TestTaskRoxo"));
                return new TestTaskRoxoDbContext(ob.Options);
            });            
        }
    }
}
