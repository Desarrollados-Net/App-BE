using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesPlus.Migrations
{
    /// <inheritdoc />
    public partial class addentityserieandentitylistaSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppListaSeguimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppListaSeguimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FechaLanzamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FotoPortada = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PaisOrigen = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CalificacionIMBD = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Escritor = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Actores = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ListaSeguimientoId = table.Column<int>(type: "int", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSeries_AppListaSeguimiento_ListaSeguimientoId",
                        column: x => x.ListaSeguimientoId,
                        principalTable: "AppListaSeguimiento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSeries_ListaSeguimientoId",
                table: "AppSeries",
                column: "ListaSeguimientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSeries");

            migrationBuilder.DropTable(
                name: "AppListaSeguimiento");
        }
    }
}
