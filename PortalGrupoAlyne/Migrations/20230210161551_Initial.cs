using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Concorrente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concorrente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrupoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoProduto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrupoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaginaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginaBase", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProdutoConcorrente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodProduto = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeProduto = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodConcorrente = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeConcorrente = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodProdutoConcorrente = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeProdutoSimilar = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoConcorrente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TabelaPreco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicial = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPreco", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoNegociacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNegociacao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeCompleto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VerificationToken = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VerifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrupoId = table.Column<int>(type: "int", nullable: true),
                    Funcao = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemURL = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimeiroLoginAdm = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Regiao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtuaCompras = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrupoProdutoId = table.Column<int>(type: "int", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_GrupoProduto_GrupoProdutoId",
                        column: x => x.GrupoProdutoId,
                        principalTable: "GrupoProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuPermissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    GrupoUsuarioId = table.Column<int>(type: "int", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPermissao_GrupoUsuario_GrupoUsuarioId",
                        column: x => x.GrupoUsuarioId,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuPermissao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parceiro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPessoa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeFantasia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj_Cpf = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fone = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Canal = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Classificacao = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TamanhoLoja = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UF = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Long = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SemVisita = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PrimeiraSem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SegundaSem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TerceiraSem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuartaSem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuintaSem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Segunda = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Terca = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Quarta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Quinta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sexta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sabado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TipoNegociacao = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Empresa = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiro", x => x.id);
                    table.ForeignKey(
                        name: "FK_Parceiro_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemTabela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TabelaPrecoId = table.Column<int>(type: "int", nullable: false),
                    IdProd = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTabela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTabela_Produto_IdProd",
                        column: x => x.IdProd,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTabela_TabelaPreco_TabelaPrecoId",
                        column: x => x.TabelaPrecoId,
                        principalTable: "TabelaPreco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pagina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    SubMenuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagina_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pagina_SubMenu_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenu",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubMenuPermissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuPermissaoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    GrupoUsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenuPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenuPermissao_GrupoUsuario_GrupoUsuarioId",
                        column: x => x.GrupoUsuarioId,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubMenuPermissao_MenuPermissao_MenuPermissaoId",
                        column: x => x.MenuPermissaoId,
                        principalTable: "MenuPermissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubMenuPermissao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TabelaPrecoCliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodEmpresa = table.Column<int>(type: "int", nullable: false),
                    CodParceiro = table.Column<int>(type: "int", nullable: false),
                    CodTabelaPreco = table.Column<int>(type: "int", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPrecoCliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoCliente_Parceiro_CodParceiro",
                        column: x => x.CodParceiro,
                        principalTable: "Parceiro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoCliente_TabelaPreco_CodTabelaPreco",
                        column: x => x.CodTabelaPreco,
                        principalTable: "TabelaPreco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaginaPermissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuPermissaoId = table.Column<int>(type: "int", nullable: true),
                    SubMenuPermissaoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    GrupoUsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginaPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaginaPermissao_GrupoUsuario_GrupoUsuarioId",
                        column: x => x.GrupoUsuarioId,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaginaPermissao_MenuPermissao_MenuPermissaoId",
                        column: x => x.MenuPermissaoId,
                        principalTable: "MenuPermissao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaginaPermissao_SubMenuPermissao_SubMenuPermissaoId",
                        column: x => x.SubMenuPermissaoId,
                        principalTable: "SubMenuPermissao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaginaPermissao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Empresa",
                columns: new[] { "Id", "AtualizadoEm", "Descricao" },
                values: new object[,]
                {
                    { 1, null, "Indústria" },
                    { 2, null, "Distribuidora" }
                });

            migrationBuilder.InsertData(
                table: "GrupoUsuario",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Administrativo" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Ordem" },
                values: new object[] { 1, 1, "fa fa-bank", "Administrativo", 0 });

            migrationBuilder.InsertData(
                table: "PaginaBase",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Url" },
                values: new object[,]
                {
                    { 1, 1, "fa fa-bank", "Administrativo", "" },
                    { 2, 2, "fa fa-bar-chart", "Trade", "" },
                    { 3, 3, "fa fa-money", "Vendas", "" },
                    { 4, 4, "fa fa-address-card", "Cadastros", "" },
                    { 5, 5, "fa fa-map-o", "Movimentos", "" },
                    { 6, 6, "fa fa-object-ungroup", "Outros", "" },
                    { 7, 7, "fa fa-search-minus", "Consultas", "" },
                    { 8, 8, "fa fa-file-o", "Relatorios", "" },
                    { 9, 9, "fa fa-user-circle-o", "Usuarios", "/cadastro-usuarios" },
                    { 10, 10, "fa fa-users", "Grupo de Usuarios", "/cadastro-grupo-usuarios" },
                    { 11, 11, "fa fa-cart-plus", "Produtos", "/cadastro-produtos" },
                    { 12, 12, "fa fa-shopping-bag", "Grupo de Produtos", "/cadastro-grupos-produtos" },
                    { 13, 13, "fa fa-user-plus", "Vendedores", "/cadastro-vendedores" },
                    { 14, 14, "fa fa-users", "Parceiros", "/cadastro-parceiros" },
                    { 15, 15, "fa fa-briefcase", "Empresas", "/cadastro-tipo-empresa" },
                    { 16, 16, "fa fa-user-times", "Concorrentes", "/cadastro-concorrentes" },
                    { 17, 17, "fa fa-user-times", "Produto x Concorrente", "/produtos-concorrentes" },
                    { 18, 18, "fa fa-credit-card", "Tipo de Negociação", "/cadastro-tipo-negociacao" },
                    { 19, 19, "fa fa-calculator", "Tabela de Preço", "/tabela-de-preco" },
                    { 20, 20, "fa fa-calculator", "Tabela de Preço Cliente", "/tabela-de-preco-cliente" },
                    { 21, 21, "fa fa-id-card-o", "Cadastro de Páginas", "/cadastro-de-paginas" },
                    { 22, 22, "fa fa-newspaper-o", "Montar Menu", "/montar-menu" },
                    { 23, 23, "fa fa-line-chart", "Pedido de Vendas", "/pedido_vendas" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "Funcao", "GrupoId", "ImagemURL", "NomeCompleto", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PrimeiroLoginAdm", "RefreshToken", "ResetTokenExpires", "Status", "Telefone", "TokenCreated", "TokenExpires", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { 1, "nfe@grupoalyne.com.br", "Administrador do Sistema", 1, "", "Administrador Grupo Alyne", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, null, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, true, "", null, "1", "(85) 3521-8888", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", null, null });

            migrationBuilder.InsertData(
                table: "SubMenu",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "Ordem" },
                values: new object[,]
                {
                    { 1, 4, "fa fa-address-card", 1, "Cadastros", 0 },
                    { 2, 5, "fa fa-map-o", 1, "Movimentos", 0 },
                    { 3, 7, "fa fa-search-minus", 1, "Consultas", 0 },
                    { 4, 6, "fa fa-object-ungroup", 1, "Outros", 0 }
                });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 1, 13, "fa fa-briefcase", 1, "Empresas", 1, "/cadastro-tipo-empresa" },
                    { 2, 13, "fa fa-user-plus", 1, "Vendedores", 1, "/cadastro-vendedores" },
                    { 3, 18, "fa fa-credit-card", 1, "Tipo de Negociação", 1, "/cadastro-tipo-negociacao" },
                    { 4, 14, "fa fa-users", 1, "Parceiros", 1, "/cadastro-parceiros" },
                    { 5, 12, "fa fa-shopping-bag", 1, "Grupo de Produtos", 1, "/cadastro-grupos-produtos" },
                    { 6, 11, "fa fa-cart-plus", 1, "Produtos", 1, "/cadastro-produtos" },
                    { 7, 16, "fa fa-user-times", 1, "Concorrentes", 1, "/cadastro-concorrentes" },
                    { 8, 17, "fa fa-user-times", 1, "Produto x Concorrente", 1, "/produtos-concorrentes" },
                    { 9, 19, "fa fa-calculator", 1, "Tabela de Preço", 1, "/tabela-de-preco" },
                    { 10, 20, "fa fa-calculator", 1, "Tabela de Preço Cliente", 1, "/tabela-de-preco-cliente" },
                    { 11, 23, "fa fa-line-chart", 1, "Pedido de Vendas", 2, "/pedido_vendas" },
                    { 12, 9, "fa fa-user-circle-o", 1, "Usuarios", 4, "/cadastro-usuarios" },
                    { 13, 10, "fa fa-users", 1, "Grupo de Usuarios", 4, "/cadastro-grupo-usuarios" },
                    { 14, 22, "fa fa-newspaper-o", 1, "Montar Menu", 4, "/montar-menu" },
                    { 15, 21, "fa fa-id-card-o", 1, "Cadastro de Páginas", 4, "/cadastro-de-paginas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemTabela_IdProd",
                table: "ItemTabela",
                column: "IdProd");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTabela_TabelaPrecoId",
                table: "ItemTabela",
                column: "TabelaPrecoId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermissao_GrupoUsuarioId",
                table: "MenuPermissao",
                column: "GrupoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermissao_UsuarioId",
                table: "MenuPermissao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagina_MenuId",
                table: "Pagina",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagina_SubMenuId",
                table: "Pagina",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaPermissao_GrupoUsuarioId",
                table: "PaginaPermissao",
                column: "GrupoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaPermissao_MenuPermissaoId",
                table: "PaginaPermissao",
                column: "MenuPermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaPermissao_SubMenuPermissaoId",
                table: "PaginaPermissao",
                column: "SubMenuPermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginaPermissao_UsuarioId",
                table: "PaginaPermissao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_VendedorId",
                table: "Parceiro",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_GrupoProdutoId",
                table: "Produto",
                column: "GrupoProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuId",
                table: "SubMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuPermissao_GrupoUsuarioId",
                table: "SubMenuPermissao",
                column: "GrupoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuPermissao_MenuPermissaoId",
                table: "SubMenuPermissao",
                column: "MenuPermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuPermissao_UsuarioId",
                table: "SubMenuPermissao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoCliente_CodParceiro",
                table: "TabelaPrecoCliente",
                column: "CodParceiro");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoCliente_CodTabelaPreco",
                table: "TabelaPrecoCliente",
                column: "CodTabelaPreco");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concorrente");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "ItemTabela");

            migrationBuilder.DropTable(
                name: "Pagina");

            migrationBuilder.DropTable(
                name: "PaginaBase");

            migrationBuilder.DropTable(
                name: "PaginaPermissao");

            migrationBuilder.DropTable(
                name: "ProdutoConcorrente");

            migrationBuilder.DropTable(
                name: "TabelaPrecoCliente");

            migrationBuilder.DropTable(
                name: "TipoNegociacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "SubMenuPermissao");

            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "TabelaPreco");

            migrationBuilder.DropTable(
                name: "GrupoProduto");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuPermissao");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
