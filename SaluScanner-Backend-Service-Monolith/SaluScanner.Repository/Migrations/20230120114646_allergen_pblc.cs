using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class allergenpblc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
