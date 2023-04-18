using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseController.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductAndAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Availabilitys_AvailabilityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AvailabilityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Availabilitys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilitys_ProductId",
                table: "Availabilitys",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilitys_Products_ProductId",
                table: "Availabilitys",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilitys_Products_ProductId",
                table: "Availabilitys");

            migrationBuilder.DropIndex(
                name: "IX_Availabilitys_ProductId",
                table: "Availabilitys");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Availabilitys");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AvailabilityId",
                table: "Products",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Availabilitys_AvailabilityId",
                table: "Products",
                column: "AvailabilityId",
                principalTable: "Availabilitys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
