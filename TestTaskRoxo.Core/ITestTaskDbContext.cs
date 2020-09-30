using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestTaskRoxo.Core.Tables;

namespace TestTaskRoxo.Core
{
    public interface ITestTaskDbContext
    {
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbOrderDetail> OrderDetails { get; set; }
        public DbSet<DbProduct> Products { get; set; }
        public DbSet<DbOrderStatus> OrderStatuses { get; set; }

        // TODO: For tests only. Bad method. For resolve this we could split base dbClass and main class.
        public DatabaseFacade Database { get; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public Int32 SaveChanges();
        public Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
