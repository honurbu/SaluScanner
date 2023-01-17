using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DBRelationrefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Products_ProductId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ProductId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Certificates");

            migrationBuilder.CreateTable(
                name: "CertificateProduct",
                columns: table => new
                {
                    CertificatesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateProduct", x => new { x.CertificatesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CertificateProduct_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificateProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificateProduct_ProductsId",
                table: "CertificateProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Certificates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ProductId",
                table: "Certificates",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Products_ProductId",
                table: "Certificates",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
