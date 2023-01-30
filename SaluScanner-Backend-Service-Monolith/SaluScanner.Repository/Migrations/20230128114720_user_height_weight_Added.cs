using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class userheightweightAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "AspNetUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");
        }
    }
}
