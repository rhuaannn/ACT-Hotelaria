using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACT_Hotelaria.Infra.Migrations
{
    /// <inheritdoc />
    public partial class entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoicings",
                schema: "Hotelaria",
                table: "Invoicings");

            migrationBuilder.RenameTable(
                name: "Invoicings",
                schema: "Hotelaria",
                newName: "Faturamentos",
                newSchema: "Hotelaria");

            migrationBuilder.RenameColumn(
                name: "ValueTotal",
                schema: "Hotelaria",
                table: "Faturamentos",
                newName: "ValorTotalNota");

            migrationBuilder.AddColumn<decimal>(
                name: "AgreedDailyRate",
                schema: "Hotelaria",
                table: "Reservas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEmissao",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalConsumo",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiarias",
                schema: "Hotelaria",
                table: "Faturamentos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faturamentos",
                schema: "Hotelaria",
                table: "Faturamentos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Produtos",
                schema: "Hotelaria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    PrecoAtual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumos",
                schema: "Hotelaria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitarioPago = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumos_Produtos_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Hotelaria",
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumos_Reservas_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "Hotelaria",
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faturamentos_ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_ProductId",
                schema: "Hotelaria",
                table: "Consumos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_ReservationId",
                schema: "Hotelaria",
                table: "Consumos",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturamentos_Reservas_ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos",
                column: "ReservationId",
                principalSchema: "Hotelaria",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturamentos_Reservas_ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropTable(
                name: "Consumos",
                schema: "Hotelaria");

            migrationBuilder.DropTable(
                name: "Produtos",
                schema: "Hotelaria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faturamentos",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropIndex(
                name: "IX_Faturamentos_ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropColumn(
                name: "AgreedDailyRate",
                schema: "Hotelaria",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "DataEmissao",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropColumn(
                name: "TotalConsumo",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.DropColumn(
                name: "TotalDiarias",
                schema: "Hotelaria",
                table: "Faturamentos");

            migrationBuilder.RenameTable(
                name: "Faturamentos",
                schema: "Hotelaria",
                newName: "Invoicings",
                newSchema: "Hotelaria");

            migrationBuilder.RenameColumn(
                name: "ValorTotalNota",
                schema: "Hotelaria",
                table: "Invoicings",
                newName: "ValueTotal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Hotelaria",
                table: "Invoicings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "Hotelaria",
                table: "Invoicings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoicings",
                schema: "Hotelaria",
                table: "Invoicings",
                column: "Id");
        }
    }
}
