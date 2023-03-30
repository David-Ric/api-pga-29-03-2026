using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Conectado",
                table: "Usuario",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceiverId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Body = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Lida = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Usuario_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 239, 34, 245, 78, 19, 100, 150, 227, 182, 90, 27, 120, 136, 64, 17, 230, 163, 203, 57, 178, 15, 195, 12, 152, 96, 185, 34, 216, 149, 153, 156, 162, 214, 221, 19, 15, 75, 73, 104, 244, 173, 37, 157, 41, 198, 111, 98, 227, 55, 125, 114, 250, 105, 41, 46, 175, 52, 158, 145, 211, 187, 247, 102 }, new byte[] { 3, 214, 246, 164, 46, 71, 12, 180, 122, 16, 215, 177, 183, 231, 161, 124, 210, 33, 231, 9, 163, 54, 197, 73, 133, 216, 238, 227, 24, 40, 106, 215 } });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UsuarioId",
                table: "Message",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UsuarioId1",
                table: "Message",
                column: "UsuarioId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropColumn(
                name: "Conectado",
                table: "Usuario");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 122, 201, 206, 232, 216, 116, 112, 175, 27, 35, 134, 229, 41, 150, 219, 172, 234, 108, 237, 168, 25, 88, 245, 229, 237, 159, 76, 254, 86, 87, 72, 22, 109, 197, 61, 53, 86, 180, 78, 186, 164, 226, 60, 192, 249, 144, 19, 74, 231, 144, 197, 45, 125, 171, 74, 167, 151, 252, 148, 135, 84, 180, 25, 237 }, new byte[] { 152, 7, 188, 202, 89, 91, 3, 241, 80, 79, 11, 116, 245, 155, 195, 152, 164, 217, 62, 171, 163, 155, 79, 235, 36, 50, 248, 224, 197, 245, 43, 119 } });
        }
    }
}
