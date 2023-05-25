using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class TabelaPrecoAdicional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardDashVendedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MetaMes = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    VendaMes = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    VaorTotalAno = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    QuantFaturar = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ValorFaturar = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    QuantPedidoOrcamento = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ValorPedidoOrcamento = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    QuantPedido = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ValorPedido = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ClienteSemVenda = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDashVendedor", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grafico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnoAtual = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    AnoAnterior = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafico", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioClienteQueda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor02 = table.Column<int>(type: "int", nullable: true),
                    Valor03 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor04 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor05 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor06 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor07 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor08 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor09 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor10 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor11 = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioClienteQueda", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioListaCobranca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor02 = table.Column<int>(type: "int", nullable: true),
                    Valor03 = table.Column<int>(type: "int", nullable: true),
                    Valor04 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor05 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor06 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor07 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor08 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor09 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor10 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor11 = table.Column<int>(type: "int", nullable: true),
                    Valor12 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor13 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor14 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor15 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioListaCobranca", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioMetaXrealizado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor02 = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioMetaXrealizado", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioPedidoFaturar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<int>(type: "int", nullable: true),
                    Valor02 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor03 = table.Column<int>(type: "int", nullable: true),
                    Valor04 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor05 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor06 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor07 = table.Column<int>(type: "int", nullable: true),
                    Valor08 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor09 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor10 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor11 = table.Column<int>(type: "int", nullable: true),
                    Valor12 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor13 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor14 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor15 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor16 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor17 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor18 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor19 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioPedidoFaturar", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioVendaClientesCrescimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor02 = table.Column<int>(type: "int", nullable: true),
                    Valor03 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor04 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor05 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor06 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor07 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor08 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor09 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor10 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor11 = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioVendaClientesCrescimento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioVendaProdutoCrescimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<int>(type: "int", nullable: true),
                    Valor02 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor03 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor04 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor05 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor06 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor07 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor08 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor09 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor10 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor11 = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioVendaProdutoCrescimento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelatorioVendaProdutoQueda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor01 = table.Column<int>(type: "int", nullable: true),
                    Valor02 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor03 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor04 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor05 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor06 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor07 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor08 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor09 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor10 = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Valor11 = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioVendaProdutoQueda", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TabelaPrecoAdicional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    IdProd = table.Column<int>(type: "int", nullable: false),
                    ParceiroId = table.Column<int>(type: "int", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPrecoAdicional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoAdicional_Produto_IdProd",
                        column: x => x.IdProd,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendaxMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Month = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Meta = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Actual = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaxMeta", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 40, 17, 95, 246, 105, 185, 236, 166, 84, 215, 159, 79, 238, 76, 132, 113, 157, 107, 45, 99, 69, 117, 5, 137, 229, 142, 225, 248, 27, 224, 100, 172, 46, 160, 206, 35, 237, 68, 93, 147, 81, 9, 81, 59, 196, 149, 23, 215, 251, 54, 102, 79, 60, 125, 47, 35, 170, 13, 124, 74, 86, 127, 255, 46 }, new byte[] { 223, 67, 182, 210, 196, 9, 194, 195, 130, 43, 128, 33, 199, 207, 224, 6, 242, 212, 18, 190, 248, 166, 71, 148, 135, 75, 121, 202, 87, 87, 48, 48 } });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoAdicional_IdProd",
                table: "TabelaPrecoAdicional",
                column: "IdProd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDashVendedor");

            migrationBuilder.DropTable(
                name: "Grafico");

            migrationBuilder.DropTable(
                name: "RelatorioClienteQueda");

            migrationBuilder.DropTable(
                name: "RelatorioListaCobranca");

            migrationBuilder.DropTable(
                name: "RelatorioMetaXrealizado");

            migrationBuilder.DropTable(
                name: "RelatorioPedidoFaturar");

            migrationBuilder.DropTable(
                name: "RelatorioVendaClientesCrescimento");

            migrationBuilder.DropTable(
                name: "RelatorioVendaProdutoCrescimento");

            migrationBuilder.DropTable(
                name: "RelatorioVendaProdutoQueda");

            migrationBuilder.DropTable(
                name: "TabelaPrecoAdicional");

            migrationBuilder.DropTable(
                name: "VendaxMeta");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 26, 192, 66, 255, 36, 153, 181, 25, 254, 50, 58, 62, 49, 193, 165, 74, 180, 247, 238, 239, 112, 73, 15, 170, 102, 152, 29, 17, 201, 253, 222, 142, 190, 214, 254, 18, 164, 122, 194, 68, 71, 174, 12, 229, 62, 204, 240, 147, 101, 35, 203, 106, 183, 6, 58, 58, 243, 13, 67, 50, 242, 150, 146, 201 }, new byte[] { 227, 51, 139, 218, 224, 60, 167, 217, 124, 142, 60, 6, 113, 29, 182, 29, 33, 173, 133, 254, 246, 75, 122, 61, 145, 233, 58, 74, 194, 169, 188, 200 } });
        }
    }
}
