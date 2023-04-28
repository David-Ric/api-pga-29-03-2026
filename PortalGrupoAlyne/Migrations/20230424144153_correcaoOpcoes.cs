using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class correcaoOpcoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ligacaoTabela_Modulo_ModuloId",
                table: "ligacaoTabela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ligacaoTabela",
                table: "ligacaoTabela");

            migrationBuilder.RenameTable(
                name: "ligacaoTabela",
                newName: "LigacaoTabela");

            migrationBuilder.RenameIndex(
                name: "IX_ligacaoTabela_ModuloId",
                table: "LigacaoTabela",
                newName: "IX_LigacaoTabela_ModuloId");

            migrationBuilder.AddColumn<string>(
                name: "NomeCampo",
                table: "OpcaoCampo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LigacaoTabela",
                table: "LigacaoTabela",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 201, 96, 13, 93, 215, 185, 127, 253, 199, 34, 86, 153, 51, 125, 99, 216, 25, 154, 169, 51, 76, 153, 33, 92, 63, 152, 100, 71, 57, 25, 219, 177, 159, 187, 254, 204, 41, 6, 161, 207, 156, 174, 11, 159, 108, 78, 163, 52, 170, 50, 163, 237, 36, 24, 227, 50, 216, 235, 72, 11, 248, 204, 89, 16 }, new byte[] { 210, 7, 60, 182, 182, 17, 49, 199, 3, 117, 185, 200, 134, 94, 19, 172, 56, 236, 39, 132, 168, 207, 157, 231, 200, 136, 53, 22, 153, 32, 146, 128 } });

            migrationBuilder.AddForeignKey(
                name: "FK_LigacaoTabela_Modulo_ModuloId",
                table: "LigacaoTabela",
                column: "ModuloId",
                principalTable: "Modulo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigacaoTabela_Modulo_ModuloId",
                table: "LigacaoTabela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LigacaoTabela",
                table: "LigacaoTabela");

            migrationBuilder.DropColumn(
                name: "NomeCampo",
                table: "OpcaoCampo");

            migrationBuilder.RenameTable(
                name: "LigacaoTabela",
                newName: "ligacaoTabela");

            migrationBuilder.RenameIndex(
                name: "IX_LigacaoTabela_ModuloId",
                table: "ligacaoTabela",
                newName: "IX_ligacaoTabela_ModuloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ligacaoTabela",
                table: "ligacaoTabela",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 78, 201, 248, 237, 213, 3, 107, 22, 109, 51, 219, 179, 111, 73, 253, 79, 106, 216, 52, 110, 214, 215, 156, 121, 40, 194, 238, 238, 202, 158, 135, 3, 32, 128, 213, 159, 236, 16, 65, 76, 127, 164, 51, 123, 51, 247, 29, 0, 182, 145, 40, 1, 25, 62, 25, 14, 253, 72, 242, 102, 94, 204, 245, 168 }, new byte[] { 251, 24, 95, 125, 58, 172, 43, 104, 232, 144, 82, 225, 86, 170, 43, 177, 52, 74, 75, 111, 232, 96, 15, 207, 186, 47, 169, 91, 144, 141, 161, 54 } });

            migrationBuilder.AddForeignKey(
                name: "FK_ligacaoTabela_Modulo_ModuloId",
                table: "ligacaoTabela",
                column: "ModuloId",
                principalTable: "Modulo",
                principalColumn: "Id");
        }
    }
}
