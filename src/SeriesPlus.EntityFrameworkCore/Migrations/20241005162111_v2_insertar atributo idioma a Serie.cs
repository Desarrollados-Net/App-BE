using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesPlus.Migrations
{
    /// <inheritdoc />
    public partial class v2_insertaratributoidiomaaSerie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Idioma",
                table: "AppSeries",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idioma",
                table: "AppSeries");
        }
    }
}
