using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations.PostgreSQL
{
    public partial class sp_GetProductByCategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var function = @"
                CREATE OR REPLACE FUNCTION sp_getproductsbycategoryname(par TEXT)
                RETURNS TABLE(LIKE ""Products"")
                AS $$

                BEGIN

                RETURN QUERY
                    WITH RECURSIVE cte AS (
                        SELECT * FROM ""Categories"" a
                        WHERE a.""Name"" = (par)
                        UNION ALL
                        SELECT t.* FROM ""Categories"" t
                        INNER JOIN cte c ON t.""ParentId"" = c.""Id""
                    )

                    SELECT p.* FROM ""Products"" p
                    WHERE ""CategoryId"" IN (SELECT ""Id"" FROM CTE);

                END
                $$ LANGUAGE plpgsql;
            ";

            migrationBuilder.Sql(function);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var function = "DROP FUNCTION sp_getproductsbycategoryname";

            migrationBuilder.Sql(function);
        }
    }
}
