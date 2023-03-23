using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    /// <inheritdoc />
    public partial class backuptables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllergySymptom",
                table: "Allergens");

            migrationBuilder.AddColumn<string>(
                name: "ComponentDescription",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentDescription",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "AllergySymptom",
                table: "Allergens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
