using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class spGetProductsFromCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = 
            @"CREATE PROCEDURE spGetProductsFromCategoryId
                @Id int
                AS
                BEGIN
                WITH ret AS
                (
                    SELECT * FROM dbo.Categories
                    WHERE Id = 5
                    UNION ALL
                    SELECT t.* FROM dbo.Categories t 
                    INNER JOIN ret r ON t.ParentId = r.Id
                )

                SELECT p.*, ts.totalQty AS TotalQty FROM dbo.Products p
				OUTER APPLY(SELECT SUM(Stock.Qty) totalQty FROM Stock WHERE Stock.ProductId = p.Id) ts
				WHERE CategoryId IN (SELECT Id FROM ret);
                
                END";
            
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {   
            string procedure = @"DROP PROCEDURE spGetCategoryChildrens";
            
            migrationBuilder.Sql(procedure);
        }
    }
}
