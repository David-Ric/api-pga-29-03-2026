using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class saldoCredito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Sc",
                table: "Parceiro",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 19, 195, 106, 243, 108, 113, 56, 105, 174, 255, 200, 204, 189, 28, 194, 153, 143, 87, 182, 199, 20, 81, 86, 12, 90, 138, 54, 180, 84, 52, 1, 96, 199, 24, 171, 1, 200, 139, 55, 107, 177, 85, 136, 250, 139, 138, 27, 214, 190, 138, 96, 129, 7, 216, 126, 92, 215, 34, 62, 102, 116, 26, 241, 179 }, new byte[] { 108, 38, 187, 34, 49, 230, 235, 85, 120, 58, 198, 145, 125, 253, 227, 3, 132, 215, 167, 238, 149, 208, 65, 155, 184, 95, 127, 185, 166, 51, 168, 119 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sc",
                table: "Parceiro");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 95, 134, 17, 120, 209, 19, 245, 81, 213, 238, 250, 138, 216, 34, 199, 68, 221, 69, 248, 169, 0, 25, 247, 138, 145, 176, 236, 103, 220, 101, 107, 187, 54, 224, 132, 147, 226, 155, 249, 173, 150, 182, 242, 53, 167, 82, 146, 192, 231, 91, 126, 0, 144, 102, 41, 202, 82, 35, 40, 94, 217, 244, 63, 19 }, new byte[] { 63, 215, 5, 250, 47, 95, 22, 4, 205, 66, 154, 148, 25, 38, 67, 166, 51, 205, 253, 146, 39, 111, 231, 62, 220, 225, 210, 33, 164, 21, 21, 110 } });
        }
    }
}
