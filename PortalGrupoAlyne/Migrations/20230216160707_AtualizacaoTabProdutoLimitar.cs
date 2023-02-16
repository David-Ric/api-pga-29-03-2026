using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class AtualizacaoTabProdutoLimitar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoUnid2",
                table: "Produto",
                type: "varchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TipoUnid",
                table: "Produto",
                type: "varchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoUnid2",
                table: "Produto",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldMaxLength: 4,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TipoUnid",
                table: "Produto",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldMaxLength: 4,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
