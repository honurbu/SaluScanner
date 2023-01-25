using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class allergenuserrelationshipAddproductimageadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_AspNetUsers_UserId",
                table: "Allergens");

            migrationBuilder.DropIndex(
                name: "IX_Allergens_UserId",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Allergens");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AllergenUser",
                columns: table => new
                {
                    AllergiesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergenUser", x => new { x.AllergiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AllergenUser_Allergens_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergenUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergenUser_UsersId",
                table: "AllergenUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergenUser");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Allergens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_UserId",
                table: "Allergens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_AspNetUsers_UserId",
                table: "Allergens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
