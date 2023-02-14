using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class tabelaPrecoParceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.CreateTable(
                name: "TabelaPrecoParceiro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    ParceiroId = table.Column<int>(type: "int", nullable: false),
                    TabelaPrecoId = table.Column<int>(type: "int", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPrecoParceiro", x => x.id);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoParceiro_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoParceiro_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoParceiro_TabelaPreco_TabelaPrecoId",
                        column: x => x.TabelaPrecoId,
                        principalTable: "TabelaPreco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoParceiro_EmpresaId",
                table: "TabelaPrecoParceiro",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoParceiro_ParceiroId",
                table: "TabelaPrecoParceiro",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoParceiro_TabelaPrecoId",
                table: "TabelaPrecoParceiro",
                column: "TabelaPrecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaPrecoParceiro");

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 10, 20, "fa fa-calculator", 1, "Tabela de Preço Cliente", 1, "/tabela-de-preco-cliente" });
        }
    }
}
