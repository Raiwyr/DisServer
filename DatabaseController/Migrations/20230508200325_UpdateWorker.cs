using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseController.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isAdmin",
                table: "Workers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "Workers");
        }
    }
}
