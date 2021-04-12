using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class spGetCategoryHierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure =
            @"CREATE PROCEDURE spGetCategoryHierarchy
                @Name nvarchar(100)
                AS
                BEGIN
                WITH parents AS
				(
					SELECT * FROM dbo.Categories
					WHERE Name = @Name
					UNION ALL
					SELECT t.* FROM dbo.Categories t 
					INNER JOIN parents p ON p.ParentId = t.Id
				)

				SELECT * FROM parents
				UNION 
				SELECT * FROM dbo.Categories
				WHERE ParentId = (SELECT Id FROM dbo.Categories WHERE Name = @Name)

                END";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spGetCategoryHierarchy";

            migrationBuilder.Sql(procedure);
        }
    }
}
