using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class sessao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Online = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 97, 162, 252, 139, 22, 210, 31, 5, 72, 243, 188, 3, 184, 153, 64, 45, 21, 31, 188, 159, 49, 24, 63, 178, 150, 214, 237, 24, 242, 178, 217, 148, 199, 213, 0, 33, 42, 13, 209, 169, 247, 227, 220, 169, 241, 4, 100, 155, 136, 45, 80, 24, 67, 132, 177, 4, 111, 219, 77, 90, 129, 173, 40, 229 }, new byte[] { 135, 200, 61, 169, 226, 160, 90, 195, 112, 56, 247, 216, 171, 136, 214, 120, 74, 29, 200, 194, 76, 4, 78, 132, 244, 220, 75, 6, 144, 35, 211, 21 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessao");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 40, 17, 95, 246, 105, 185, 236, 166, 84, 215, 159, 79, 238, 76, 132, 113, 157, 107, 45, 99, 69, 117, 5, 137, 229, 142, 225, 248, 27, 224, 100, 172, 46, 160, 206, 35, 237, 68, 93, 147, 81, 9, 81, 59, 196, 149, 23, 215, 251, 54, 102, 79, 60, 125, 47, 35, 170, 13, 124, 74, 86, 127, 255, 46 }, new byte[] { 223, 67, 182, 210, 196, 9, 194, 195, 130, 43, 128, 33, 199, 207, 224, 6, 242, 212, 18, 190, 248, 166, 71, 148, 135, 75, 121, 202, 87, 87, 48, 48 } });
        }
    }
}
