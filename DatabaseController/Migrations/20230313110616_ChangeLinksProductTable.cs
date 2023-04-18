using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseController.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLinksProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Contraindications_ContraindicationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Indications_IndicationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ContraindicationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IndicationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContraindicationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IndicationId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ContraindicationProduct",
                columns: table => new
                {
                    ContraindicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContraindicationProduct", x => new { x.ContraindicationId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ContraindicationProduct_Contraindications_ContraindicationId",
                        column: x => x.ContraindicationId,
                        principalTable: "Contraindications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContraindicationProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicationProduct",
                columns: table => new
                {
                    IndicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicationProduct", x => new { x.IndicationId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_IndicationProduct_Indications_IndicationId",
                        column: x => x.IndicationId,
                        principalTable: "Indications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicationProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContraindicationProduct_ProductsId",
                table: "ContraindicationProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicationProduct_ProductsId",
                table: "IndicationProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContraindicationProduct");

            migrationBuilder.DropTable(
                name: "IndicationProduct");

            migrationBuilder.AddColumn<int>(
                name: "ContraindicationId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IndicationId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ContraindicationId",
                table: "Products",
                column: "ContraindicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IndicationId",
                table: "Products",
                column: "IndicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Contraindications_ContraindicationId",
                table: "Products",
                column: "ContraindicationId",
                principalTable: "Contraindications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Indications_IndicationId",
                table: "Products",
                column: "IndicationId",
                principalTable: "Indications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
