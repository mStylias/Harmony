using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "store_id",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_store_id",
                table: "products",
                column: "store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_stores_store_id",
                table: "products",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_stores_store_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_store_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "products");
        }
    }
}
