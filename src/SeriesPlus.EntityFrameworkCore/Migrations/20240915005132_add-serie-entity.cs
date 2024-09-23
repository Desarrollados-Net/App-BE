using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesPlus.Migrations
{
    /// <inheritdoc />
    public partial class addserieentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    PaisOrigen = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CalificacionIMBD = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Escritor = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Actores = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSeries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSeries");
        }
    }
}
