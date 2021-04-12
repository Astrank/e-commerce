using Microsoft.EntityFrameworkCore.Migrations;

namespace EPE.Database.Migrations
{
    public partial class categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImage_Projects_ProjectId",
                table: "ProjectImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImage",
                table: "ProjectImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProjectImage",
                newName: "ProjectImages");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImage_ProjectId",
                table: "ProjectImages",
                newName: "IX_ProjectImages_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Products",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubcategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategories_ProductCategoryId",
                table: "ProductSubcategories",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSubcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "ProductSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSubcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages");

            migrationBuilder.DropTable(
                name: "ProductSubcategories");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ProjectImages",
                newName: "ProjectImage");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImage");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImage",
                newName: "IX_ProjectImage_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImage",
                table: "ProjectImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImage_Projects_ProjectId",
                table: "ProjectImage",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
