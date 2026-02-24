using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACT_Hotelaria.Infra.Migrations
{
    /// <inheritdoc />
    public partial class removeTotalPriceEntitiesReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "Hotelaria",
                table: "Reservas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "Hotelaria",
                table: "Reservas",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
