using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class alteracaoParceiros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaginaBase",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendedor",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "codParceiro",
                table: "Parceiro",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codParceiro",
                table: "Parceiro");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendedor",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PaginaBase",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Url" },
                values: new object[] { 20, 20, "fa fa-calculator", "Tabela de Preço Cliente", "/tabela-de-preco-cliente" });
        }
    }
}
