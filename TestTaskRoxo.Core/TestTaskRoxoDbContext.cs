using Microsoft.EntityFrameworkCore;
using TestTaskRoxo.Core.Tables;

namespace TestTaskRoxo.Core
{
    class TestTaskRoxoDbContext : DbContext, ITestTaskDbContext
    {
        /* FIXME: Only for generate migrations */
        public TestTaskRoxoDbContext() : base(CreateDefaultParams().Options)
        {
        }

        public static DbContextOptionsBuilder<TestTaskRoxoDbContext> CreateDefaultParams()
        {
            var prm = new DbContextOptionsBuilder<TestTaskRoxoDbContext>();
            prm.UseSqlServer("Data Source=localhost;Database=TestTaskRoxo;Integrated Security=True;MultipleActiveResultSets=true");
            return prm;
        }

        
        public TestTaskRoxoDbContext(DbContextOptions<TestTaskRoxoDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<DbOrder> Orders { get; set; }
        public virtual DbSet<DbOrderDetail> OrderDetails { get; set; }
        public virtual DbSet<DbProduct> Products { get; set; }
        public virtual DbSet<DbOrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TestTaskRoxo");
        }
    }
}
