using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class AtualizacaoTabProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Conv",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoUnid",
                table: "Produto",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoUnid2",
                table: "Produto",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conv",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TipoUnid",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TipoUnid2",
                table: "Produto");
        }
    }
}
