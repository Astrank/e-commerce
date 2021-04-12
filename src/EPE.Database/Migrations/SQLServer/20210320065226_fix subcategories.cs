using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class fixsubcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubcategories_ProductCategories_ProductCategoryId",
                table: "ProductSubcategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductSubcategories");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductSubcategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubcategories_ProductCategories_ProductCategoryId",
                table: "ProductSubcategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubcategories_ProductCategories_ProductCategoryId",
                table: "ProductSubcategories");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductSubcategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductSubcategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubcategories_ProductCategories_ProductCategoryId",
                table: "ProductSubcategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
