using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class Migracaodados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComunicadoComercial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Texto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrupoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComunicadoComercial", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComunicadoLido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LidoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComunicadoLido", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PermissaoRH",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GrupoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoRH", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostLido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LidoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLido_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PreferenciasUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Display = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico1 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico2 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico3 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico4 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico5 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico6 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico7 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico8 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico9 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grafico10 = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciasUsuario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 33,
                column: "MenuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 34,
                column: "SubMenuId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 41, 31, "fa fa-comments-o", 1, "CI - RH", 4, "/comunicacao-interna" },
                    { 42, 31, "fa fa-comments-o", 5, "CI - RH", null, "/comunicacao-interna" },
                    { 43, 32, "fa fa-comments-o", 1, "Espaço Colaborador", 4, "/espaco-colaborador" },
                    { 44, 32, "fa fa-comments-o", 5, "Espaço Colaborador", null, "/espaco-colaborador" },
                    { 45, 33, "fa fa-commenting", 1, "CI - Comercial", 4, "/comunicacao-interna-comercial" },
                    { 46, 33, "fa fa-commenting", 5, "CI - Comercial", null, "/comunicacao-interna-comercial" },
                    { 47, 34, "fa fa-money", 1, "Comissões", 3, "/comissoes" },
                    { 48, 34, "fa fa-money", 4, "Comissões", null, "/comissoes" }
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 201, 144, 133, 218, 7, 207, 40, 146, 187, 234, 185, 58, 68, 200, 23, 86, 240, 164, 174, 94, 76, 23, 73, 45, 229, 125, 129, 143, 61, 0, 30, 56, 80, 119, 107, 5, 11, 149, 156, 18, 237, 155, 97, 5, 36, 117, 200, 145, 239, 130, 212, 199, 163, 24, 166, 143, 220, 107, 193, 114, 225, 11, 53, 28 }, new byte[] { 19, 167, 27, 21, 0, 241, 36, 83, 186, 151, 59, 152, 118, 184, 76, 76, 128, 250, 183, 130, 35, 237, 250, 49, 129, 176, 66, 86, 86, 144, 1, 144 } });

            migrationBuilder.CreateIndex(
                name: "IX_PostLido_UsuarioID",
                table: "PostLido",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComunicadoComercial");

            migrationBuilder.DropTable(
                name: "ComunicadoLido");

            migrationBuilder.DropTable(
                name: "PermissaoRH");

            migrationBuilder.DropTable(
                name: "PostLido");

            migrationBuilder.DropTable(
                name: "PreferenciasUsuario");

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.UpdateData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 33,
                column: "MenuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 34,
                column: "SubMenuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 2, 45, 218, 76, 77, 54, 230, 174, 190, 178, 182, 173, 235, 121, 225, 189, 238, 64, 229, 226, 162, 1, 22, 41, 56, 191, 242, 212, 68, 173, 148, 15, 97, 231, 59, 216, 117, 88, 192, 144, 172, 20, 193, 226, 121, 135, 201, 209, 218, 121, 46, 121, 53, 111, 250, 140, 155, 166, 82, 103, 251, 67, 39, 186 }, new byte[] { 59, 47, 220, 208, 105, 21, 194, 255, 208, 238, 185, 68, 240, 107, 213, 72, 227, 59, 250, 123, 217, 251, 247, 90, 148, 159, 249, 206, 198, 207, 175, 220 } });
        }
    }
}
