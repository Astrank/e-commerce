using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class spGetProductsByCategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure =
            @"CREATE PROCEDURE spGetProductsByCategoryName
                @Name nvarchar(100)
                AS
                BEGIN
                WITH ret AS
                (
                    SELECT * FROM dbo.Categories
                    WHERE Name = @Name
                    UNION ALL
                    SELECT t.* FROM dbo.Categories t 
                    INNER JOIN ret r ON t.ParentId = r.Id
                )

                SELECT p.* FROM dbo.Products p
				WHERE CategoryId IN (SELECT Id FROM ret);

                END";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spGetProductsByCategoryName";

            migrationBuilder.Sql(procedure);
        }
    }
}
