using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class colunaModeloAutomatico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Automatico",
                table: "ColunaModulo",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 26, 192, 66, 255, 36, 153, 181, 25, 254, 50, 58, 62, 49, 193, 165, 74, 180, 247, 238, 239, 112, 73, 15, 170, 102, 152, 29, 17, 201, 253, 222, 142, 190, 214, 254, 18, 164, 122, 194, 68, 71, 174, 12, 229, 62, 204, 240, 147, 101, 35, 203, 106, 183, 6, 58, 58, 243, 13, 67, 50, 242, 150, 146, 201 }, new byte[] { 227, 51, 139, 218, 224, 60, 167, 217, 124, 142, 60, 6, 113, 29, 182, 29, 33, 173, 133, 254, 246, 75, 122, 61, 145, 233, 58, 74, 194, 169, 188, 200 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Automatico",
                table: "ColunaModulo");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 201, 96, 13, 93, 215, 185, 127, 253, 199, 34, 86, 153, 51, 125, 99, 216, 25, 154, 169, 51, 76, 153, 33, 92, 63, 152, 100, 71, 57, 25, 219, 177, 159, 187, 254, 204, 41, 6, 161, 207, 156, 174, 11, 159, 108, 78, 163, 52, 170, 50, 163, 237, 36, 24, 227, 50, 216, 235, 72, 11, 248, 204, 89, 16 }, new byte[] { 210, 7, 60, 182, 182, 17, 49, 199, 3, 117, 185, 200, 134, 94, 19, 172, 56, 236, 39, 132, 168, 207, 157, 231, 200, 136, 53, 22, 153, 32, 146, 128 } });
        }
    }
}
