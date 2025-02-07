using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoShopRentalV3.Data.Migrations
{
    /// <inheritdoc />
    public partial class editRentalHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalHeaderId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RentalHeaderId",
                table: "Movies",
                column: "RentalHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_RentalHeaders_RentalHeaderId",
                table: "Movies",
                column: "RentalHeaderId",
                principalTable: "RentalHeaders",
                principalColumn: "RentalHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_RentalHeaders_RentalHeaderId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RentalHeaderId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RentalHeaderId",
                table: "Movies");
        }
    }
}
