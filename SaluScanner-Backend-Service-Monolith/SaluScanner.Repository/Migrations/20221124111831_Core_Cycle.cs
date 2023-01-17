using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CoreCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Contents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ProductId",
                table: "Contents",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Products_ProductId",
                table: "Contents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Products_ProductId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ProductId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Contents");

            migrationBuilder.CreateTable(
                name: "ContentProduct",
                columns: table => new
                {
                    ContentsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentProduct", x => new { x.ContentsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ContentProduct_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentProduct_ProductsId",
                table: "ContentProduct",
                column: "ProductsId");
        }
    }
}
