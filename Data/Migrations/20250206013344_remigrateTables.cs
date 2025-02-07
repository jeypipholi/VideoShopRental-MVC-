using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoShopRentalV3.Data.Migrations
{
    /// <inheritdoc />
    public partial class remigrateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_RentalHeaders_RentalHeaderId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetails_Movies_MovieId",
                table: "RentalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHeaders_Customers_CustomerId",
                table: "RentalHeaders");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RentalHeaderId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RentalHeaderId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetails_Movies_MovieId",
                table: "RentalDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHeaders_Customers_CustomerId",
                table: "RentalHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetails_Movies_MovieId",
                table: "RentalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHeaders_Customers_CustomerId",
                table: "RentalHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetails_Movies_MovieId",
                table: "RentalDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHeaders_Customers_CustomerId",
                table: "RentalHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
