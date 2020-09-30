using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskRoxo.Core.Migrations
{
    public partial class AddTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[Product](Name) SELECT 'Test Product 1' UNION ALL SELECT 'Test Product 2'  UNION ALL SELECT 'Test Product 3'  UNION ALL SELECT 'Test Product 4'");
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[OrderStatus](Name) SELECT 'Complete' UNION ALL SELECT 'In progress'");
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[Order](DateCreated, OrderStatusId) SELECT GETDATE(), 2 UNION ALL SELECT DATEADD(dd,-1, GETDATE()), 1 UNION ALL SELECT DATEADD(dd,-4, GETDATE()), 1");
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[OrderDetail](OrderId, ProductId, Quantity, Price) SELECT 1, 2, 19, 55.14 UNION ALL SELECT 2, 1, 4, 655.94");
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[OrderDetail](OrderId, ProductId, Quantity, Price) SELECT 1, 1, 1, 15.14 UNION ALL SELECT 2, 3, 2, 11.95");
            migrationBuilder.Sql(@"INSERT INTO [TestTaskRoxo].[OrderDetail](OrderId, ProductId, Quantity, Price) SELECT 3, 1, 1, 15.14 UNION ALL SELECT 3, 2, 2, 11.95 UNION ALL SELECT 3, 3, 4, 99.99 UNION ALL SELECT 3, 4, 144, 1.01");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE [TestTaskRoxo].[Product]");
            migrationBuilder.Sql(@"TRUNCATE TABLE [TestTaskRoxo].[OrderStatus]");
            migrationBuilder.Sql(@"TRUNCATE TABLE [TestTaskRoxo].[Order]");
            migrationBuilder.Sql(@"TRUNCATE TABLE [TestTaskRoxo].[OrderDetail]");
        }
    }
}
