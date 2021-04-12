using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations.PostgreSQL
{
    public partial class sp_GetCategoryHierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var function = @"
                CREATE OR REPLACE FUNCTION sp_getcategoryhierarchy(name TEXT)
                RETURNS TABLE(LIKE ""Categories"")
                AS $$

                BEGIN

                RETURN QUERY
                    WITH RECURSIVE parents AS (
                        SELECT * FROM ""Categories""
                        WHERE ""Name"" = (name)
                        UNION ALL
                        SELECT t.*  FROM ""Categories"" t
                        INNER JOIN parents p ON p.""ParentId"" = t.""Id""
                    )

                    SELECT * FROM parents
                    UNION
                    SELECT * FROM ""Categories""
                    WHERE ""ParentId"" = (
                        SELECT ""Id"" FROM ""Categories"" WHERE ""Name"" = (name)
                    )
                    ORDER BY ""Id"";

                END

                $$ LANGUAGE plpgsql;
            ";

            migrationBuilder.Sql(function);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string function = @"DROP FUNCTION sp_getcategoryhierarchy";

            migrationBuilder.Sql(function);
        }
    }
}