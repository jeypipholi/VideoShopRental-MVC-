using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoShopRentalV3.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRentalHEader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieIds",
                table: "RentalHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieIds",
                table: "RentalHeaders");
        }
    }
}
