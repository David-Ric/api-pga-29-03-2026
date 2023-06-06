using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class logAcao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAcao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tabela = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Metodo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Obs = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAcao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 95, 134, 17, 120, 209, 19, 245, 81, 213, 238, 250, 138, 216, 34, 199, 68, 221, 69, 248, 169, 0, 25, 247, 138, 145, 176, 236, 103, 220, 101, 107, 187, 54, 224, 132, 147, 226, 155, 249, 173, 150, 182, 242, 53, 167, 82, 146, 192, 231, 91, 126, 0, 144, 102, 41, 202, 82, 35, 40, 94, 217, 244, 63, 19 }, new byte[] { 63, 215, 5, 250, 47, 95, 22, 4, 205, 66, 154, 148, 25, 38, 67, 166, 51, 205, 253, 146, 39, 111, 231, 62, 220, 225, 210, 33, 164, 21, 21, 110 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAcao");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 97, 162, 252, 139, 22, 210, 31, 5, 72, 243, 188, 3, 184, 153, 64, 45, 21, 31, 188, 159, 49, 24, 63, 178, 150, 214, 237, 24, 242, 178, 217, 148, 199, 213, 0, 33, 42, 13, 209, 169, 247, 227, 220, 169, 241, 4, 100, 155, 136, 45, 80, 24, 67, 132, 177, 4, 111, 219, 77, 90, 129, 173, 40, 229 }, new byte[] { 135, 200, 61, 169, 226, 160, 90, 195, 112, 56, 247, 216, 171, 136, 214, 120, 74, 29, 200, 194, 76, 4, 78, 132, 244, 220, 75, 6, 144, 35, 211, 21 } });
        }
    }
}
