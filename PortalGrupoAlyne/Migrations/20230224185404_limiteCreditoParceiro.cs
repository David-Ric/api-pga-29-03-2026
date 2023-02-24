using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class limiteCreditoParceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Lc",
                table: "Parceiro",
                type: "decimal(65,30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lc",
                table: "Parceiro");
        }
    }
}
