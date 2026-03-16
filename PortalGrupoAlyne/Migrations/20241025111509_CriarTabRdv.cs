using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class CriarTabRdv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RDVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HoraFin = table.Column<TimeSpan>(type: "varchar(5)", nullable: false),
                    NomeCliente = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraIni = table.Column<TimeSpan>(type: "varchar(5)", nullable: false),
                    UF = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Objetivo = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    km = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDVs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 205, 42, "fa fa-line-chart", 1, "Pedidos em Processo", 2, "/pedidos-em-processamento" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 206, 42, "fa fa-line-chart", 3, "Pedidos em Processo", null, "/pedidos-em-processamento" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 207, 42, "fa fa-line-chart", 1, "Rdvs", 2, "/cadastro-rdv" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 208, 42, "fa fa-line-chart", 3, "Rdvs", null, "/cadastro-rdv" });


            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 45, 246, 131, 199, 218, 31, 45, 48, 63, 47, 247, 68, 21, 191, 179, 43, 148, 34, 179, 243, 174, 92, 72, 129, 56, 160, 131, 108, 181, 150, 159, 157, 17, 79, 9, 64, 193, 120, 93, 50, 104, 184, 93, 27, 124, 64, 178, 249, 124, 179, 210, 196, 163, 168, 241, 77, 227, 164, 82, 52, 15, 1, 228 }, new byte[] { 64, 70, 115, 80, 57, 161, 58, 89, 251, 82, 112, 67, 114, 37, 17, 73, 43, 135, 201, 129, 93, 115, 94, 252, 206, 134, 170, 235, 143, 149, 204, 224 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RDVs");

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 208);


            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 62, 254, 71, 58, 27, 26, 79, 12, 194, 54, 163, 17, 77, 209, 73, 100, 246, 64, 225, 98, 158, 58, 173, 60, 1, 162, 13, 119, 234, 74, 115, 137, 57, 31, 174, 204, 82, 247, 83, 198, 27, 186, 91, 190, 224, 29, 156, 81, 39, 119, 38, 31, 92, 53, 68, 252, 146, 228, 139, 225, 80, 22, 118, 116 }, new byte[] { 8, 160, 189, 181, 66, 15, 69, 33, 246, 219, 75, 4, 117, 52, 89, 135, 0, 248, 191, 147, 180, 169, 139, 151, 204, 209, 89, 49, 172, 244, 18, 106 } });
        }
    }
}
