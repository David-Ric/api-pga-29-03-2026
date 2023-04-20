using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class CriarTelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuAdminId = table.Column<int>(type: "int", nullable: true),
                    SubMenuAdminId = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tabela = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    insert = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    update = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    delete = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    icone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    filtro1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    filtro2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    filtro3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ColunaModulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoInput = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TabInput = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueTabInput = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LabelTabInput = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChavePrimaria = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Expressao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColunaModulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColunaModulo_Modulo_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulo",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ligacaoTabela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CampoLigacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TabeaLigada = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CampoExibir = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ligacaoTabela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ligacaoTabela_Modulo_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulo",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpcaoCampo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opcao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColunaModuloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoCampo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcaoCampo_ColunaModulo_ColunaModuloId",
                        column: x => x.ColunaModuloId,
                        principalTable: "ColunaModulo",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 78, 201, 248, 237, 213, 3, 107, 22, 109, 51, 219, 179, 111, 73, 253, 79, 106, 216, 52, 110, 214, 215, 156, 121, 40, 194, 238, 238, 202, 158, 135, 3, 32, 128, 213, 159, 236, 16, 65, 76, 127, 164, 51, 123, 51, 247, 29, 0, 182, 145, 40, 1, 25, 62, 25, 14, 253, 72, 242, 102, 94, 204, 245, 168 }, new byte[] { 251, 24, 95, 125, 58, 172, 43, 104, 232, 144, 82, 225, 86, 170, 43, 177, 52, 74, 75, 111, 232, 96, 15, 207, 186, 47, 169, 91, 144, 141, 161, 54 } });

            migrationBuilder.CreateIndex(
                name: "IX_ColunaModulo_ModuloId",
                table: "ColunaModulo",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ligacaoTabela_ModuloId",
                table: "ligacaoTabela",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoCampo_ColunaModuloId",
                table: "OpcaoCampo",
                column: "ColunaModuloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ligacaoTabela");

            migrationBuilder.DropTable(
                name: "OpcaoCampo");

            migrationBuilder.DropTable(
                name: "ColunaModulo");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 94, 117, 155, 246, 160, 21, 166, 46, 105, 254, 157, 178, 110, 148, 128, 192, 87, 138, 81, 232, 19, 228, 56, 182, 203, 168, 211, 41, 183, 144, 65, 166, 117, 195, 157, 183, 116, 176, 241, 226, 106, 36, 204, 240, 94, 212, 83, 103, 110, 144, 81, 30, 124, 233, 52, 208, 246, 128, 189, 198, 124, 84, 24 }, new byte[] { 169, 87, 41, 254, 206, 108, 147, 10, 110, 84, 149, 173, 113, 9, 64, 239, 126, 197, 152, 132, 176, 8, 26, 12, 1, 129, 55, 92, 33, 115, 77, 181 } });
        }
    }
}
