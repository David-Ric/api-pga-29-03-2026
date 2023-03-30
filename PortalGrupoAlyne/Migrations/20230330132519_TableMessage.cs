using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class TableMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Message",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Message",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 94, 117, 155, 246, 160, 21, 166, 46, 105, 254, 157, 178, 110, 148, 128, 192, 87, 138, 81, 232, 19, 228, 56, 182, 203, 168, 211, 41, 183, 144, 65, 166, 117, 195, 157, 183, 116, 176, 241, 226, 106, 36, 204, 240, 94, 212, 83, 103, 110, 144, 81, 30, 124, 233, 52, 208, 246, 128, 189, 198, 124, 84, 24 }, new byte[] { 169, 87, 41, 254, 206, 108, 147, 10, 110, 84, 149, 173, 113, 9, 64, 239, 126, 197, 152, 132, 176, 8, 26, 12, 1, 129, 55, 92, 33, 115, 77, 181 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Message",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Message",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 239, 34, 245, 78, 19, 100, 150, 227, 182, 90, 27, 120, 136, 64, 17, 230, 163, 203, 57, 178, 15, 195, 12, 152, 96, 185, 34, 216, 149, 153, 156, 162, 214, 221, 19, 15, 75, 73, 104, 244, 173, 37, 157, 41, 198, 111, 98, 227, 55, 125, 114, 250, 105, 41, 46, 175, 52, 158, 145, 211, 187, 247, 102 }, new byte[] { 3, 214, 246, 164, 46, 71, 12, 180, 122, 16, 215, 177, 183, 231, 161, 124, 210, 33, 231, 9, 163, 54, 197, 73, 133, 216, 238, 227, 24, 40, 106, 215 } });
        }
    }
}
