using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class custometiquetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "printerAddress",
                table: "Etiqueta",
                newName: "PrinterAddress");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTxt",
                table: "Etiqueta",
                type: "varchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Zpl",
                table: "Etiqueta",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 6, 123, 35, 152, 49, 2, 191, 85, 119, 193, 147, 221, 160, 102, 196, 245, 201, 142, 170, 111, 34, 54, 113, 139, 111, 152, 168, 15, 97, 23, 144, 67, 30, 171, 129, 142, 236, 137, 206, 139, 251, 39, 183, 210, 200, 100, 82, 139, 164, 148, 94, 252, 197, 19, 96, 39, 235, 146, 216, 150, 255, 23, 95, 49 }, new byte[] { 210, 236, 193, 64, 2, 93, 60, 242, 50, 0, 120, 92, 91, 25, 75, 226, 252, 237, 83, 101, 130, 196, 211, 25, 249, 64, 224, 228, 118, 169, 161, 12 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zpl",
                table: "Etiqueta");

            migrationBuilder.RenameColumn(
                name: "PrinterAddress",
                table: "Etiqueta",
                newName: "printerAddress");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTxt",
                table: "Etiqueta",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(400)",
                oldMaxLength: 400,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 200, 153, 204, 142, 111, 218, 166, 120, 234, 27, 253, 195, 79, 147, 249, 215, 255, 189, 27, 33, 38, 1, 140, 46, 245, 44, 229, 215, 208, 202, 144, 208, 241, 141, 109, 255, 117, 207, 150, 82, 82, 107, 117, 138, 81, 124, 124, 219, 224, 0, 161, 205, 2, 244, 75, 248, 61, 91, 93, 164, 12, 165, 141, 208 }, new byte[] { 149, 113, 161, 89, 31, 106, 215, 133, 178, 44, 57, 85, 4, 75, 199, 142, 172, 240, 228, 18, 217, 99, 88, 106, 70, 217, 93, 239, 9, 21, 176, 245 } });
        }
    }
}
