using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACT_Hotelaria.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Hotelaria",
                table: "Reservas");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "Hotelaria",
                table: "Reservas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Quartos",
                schema: "Hotelaria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_RoomId",
                schema: "Hotelaria",
                table: "Reservas",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Quartos_RoomId",
                schema: "Hotelaria",
                table: "Reservas",
                column: "RoomId",
                principalSchema: "Hotelaria",
                principalTable: "Quartos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Quartos_RoomId",
                schema: "Hotelaria",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "Quartos",
                schema: "Hotelaria");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_RoomId",
                schema: "Hotelaria",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "Hotelaria",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Hotelaria",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
