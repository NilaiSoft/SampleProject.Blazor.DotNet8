using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnableFildToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "Products");
        }
    }
}
