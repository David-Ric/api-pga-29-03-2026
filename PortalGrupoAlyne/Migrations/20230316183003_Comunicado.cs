using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class Comunicado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunicado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemURL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imagem = table.Column<byte[]>(type: "longblob", nullable: false),
                    Texto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicado", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 112, 215, 222, 219, 254, 3, 191, 23, 9, 94, 67, 157, 150, 116, 49, 55, 144, 91, 212, 70, 134, 135, 163, 7, 130, 24, 224, 127, 196, 102, 180, 82, 25, 243, 101, 25, 245, 238, 63, 101, 147, 193, 146, 202, 180, 119, 129, 241, 28, 244, 6, 190, 83, 10, 216, 153, 125, 111, 250, 107, 102, 49, 13, 239 }, new byte[] { 27, 196, 18, 241, 202, 54, 201, 175, 45, 255, 81, 144, 199, 196, 161, 28, 4, 156, 32, 221, 70, 201, 144, 59, 176, 99, 136, 145, 138, 235, 131, 36 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comunicado");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 52, 37, 62, 80, 243, 171, 201, 64, 226, 150, 171, 96, 4, 176, 149, 248, 15, 223, 234, 222, 220, 157, 162, 121, 16, 158, 39, 74, 109, 11, 216, 3, 117, 102, 219, 200, 34, 103, 190, 199, 250, 130, 47, 82, 245, 102, 161, 17, 167, 126, 67, 223, 198, 184, 206, 248, 10, 7, 48, 119, 160, 87, 139, 78 }, new byte[] { 221, 157, 124, 1, 213, 120, 56, 54, 153, 237, 197, 7, 168, 31, 67, 198, 129, 158, 187, 245, 63, 189, 61, 105, 149, 58, 214, 79, 95, 254, 178, 4 } });
        }
    }
}
