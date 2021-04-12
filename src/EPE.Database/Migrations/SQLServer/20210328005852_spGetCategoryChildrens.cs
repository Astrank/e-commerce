using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class spGetCategoryChildrens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = 
            @"CREATE PROCEDURE spGetCategoryChildrens
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

                SELECT * FROM ret;
                
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
