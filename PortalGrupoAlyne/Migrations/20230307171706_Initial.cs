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
                name: "Configuracao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SankhyaServidor = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SankhyaUsuario = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SankhyaSenha = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Id);
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
                name: "IntegracaoSankhya",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TabelaPortal = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChaveTabelaPortal = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SqlObterSankhya = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegracaoSankhya", x => x.Id);
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
                    Email = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtuaCompras = table.Column<bool>(type: "tinyint(1)", nullable: true),
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
                    TipoUnid = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoUnid2 = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conv = table.Column<int>(type: "int", nullable: true),
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
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Usuario_GrupoUsuario_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id");
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
                name: "Parceiro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codParceiro = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPessoa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeFantasia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj_Cpf = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
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
                    Bairro = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UF = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Long = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lc = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SemVisita = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    PrimeiraSem = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    SegundaSem = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    TerceiraSem = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    QuartaSem = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    QuintaSem = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Segunda = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Terca = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Quarta = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Quinta = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Sexta = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Sabado = table.Column<bool>(type: "tinyint(1)", nullable: true),
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
                name: "CabecalhoPedidoVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Filial = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lote = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    PalMPV = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoNegociacaoId = table.Column<int>(type: "int", nullable: false),
                    ParceiroId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    DataEntrega = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Baixado = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pedido = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabecalhoPedidoVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabecalhoPedidoVenda_TipoNegociacao_TipoNegociacaoId",
                        column: x => x.TipoNegociacaoId,
                        principalTable: "TipoNegociacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabecalhoPedidoVenda_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "Titulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    ParceiroId = table.Column<int>(type: "int", nullable: true),
                    NuUnico = table.Column<int>(type: "int", nullable: true),
                    Parcela = table.Column<int>(type: "int", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataVencim = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titulo_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "id");
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
                name: "ItemPedidoVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Filial = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CabecalhoPedidoVendaId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    PalMPV = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quant = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ValUnit = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ValTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Baixado = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidoVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedidoVenda_CabecalhoPedidoVenda_CabecalhoPedidoVendaId",
                        column: x => x.CabecalhoPedidoVendaId,
                        principalTable: "CabecalhoPedidoVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidoVenda_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidoVenda_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
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
                table: "Configuracao",
                columns: new[] { "Id", "AtualizadoEm", "SankhyaSenha", "SankhyaServidor", "SankhyaUsuario" },
                values: new object[] { 1, null, "SYNC550V", "http://10.0.0.254:8280/", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Empresa",
                columns: new[] { "Id", "AtualizadoEm", "Descricao" },
                values: new object[,]
                {
                    { 1, null, "Industria" },
                    { 2, null, "Distribuidora" }
                });

            migrationBuilder.InsertData(
                table: "GrupoUsuario",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Administrativo" });

            migrationBuilder.InsertData(
                table: "IntegracaoSankhya",
                columns: new[] { "Id", "AtualizadoEm", "ChaveTabelaPortal", "SqlObterSankhya", "TabelaPortal" },
                values: new object[,]
                {
                    { 1, null, "Id", "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \r\n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\r\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId AND DTALTER > '$AtualizadoEm'", "Vendedor" },
                    { 2, null, "Id", "SELECT DISTINCT TPV.CODTIPVENDA Id, \r\n                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,\r\n                        TPV.DHALTER AtualizadoEm\r\n                    FROM TGFTPV (NOLOCK) TPV\r\n                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \r\n                                            AND VEN.CODVEND = $VendedorId\r\n                    WHERE TPV.CODTIPVENDA > 0\r\n                    AND DHALTER > '$AtualizadoEm'\r\n                    ORDER BY 1", "TipoNegociacao" },
                    { 3, null, "Id", "SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, \r\n                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, \r\n                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, \r\n                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, \r\n                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,\r\n                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,\r\n                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, \r\n                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, \r\n                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm\r\n                    FROM TGFPAR (NOLOCK) PAR\r\n					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \r\n                                            AND VEN.CODVEND = $VendedorId\r\n                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\r\n                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\r\n                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\r\n                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\r\n                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\r\n                    WHERE PAR.CLIENTE = 'S' \r\n                    AND PAR.CODPARC > 0 \r\n                    AND PAR.CODVEND > 0\r\n                    AND PAR.ATIVO = 'S'", "Parceiro" },
                    { 4, null, "Id", "SELECT convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) Id, \r\n                    RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome\r\n                    FROM sankhya.TGFGRU (NOLOCK)\r\n                    WHERE ANALITICO = 'S'\r\n                    and SUBSTRING(RTRIM(CODGRUPOPROD),1,3) = '120'", "GrupoProduto" },
                    { 5, null, "Id", "SELECT PRO.CODPROD Id, \r\n                        PRO.DESCRPROD Nome, \r\n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\r\n                        PRO.DTALTER AtualizadoEm,\r\n                        PRO.CODVOL TipoUnid,\r\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\r\n                        ISNULL(VOA.QUANTIDADE,1) Conv\r\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\r\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\r\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\r\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')\r\n                    AND PRO.DTALTER > '$AtualizadoEm'", "Produto" },
                    { 6, null, "Id", "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \r\n                    FROM TGFNTA (NOLOCK) NTA\r\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                            AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R'\r\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \r\n                    ORDER BY 1", "TabelaPreco" },
                    { 7, null, "TabelaPrecoId,IdProd", "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \r\n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\r\n                    FROM TGFTAB TAB\r\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\r\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\r\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\r\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \r\n                                            FROM TGFNTA (NOLOCK) NTA\r\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                                                    AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R' \r\n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\r\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\r\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\r\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\r\n                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\r\n                    ORDER BY TAB.CODTAB, PRO.CODPROD", "ItemTabela" },
                    { 8, null, "ParceiroId,EmpresaId,TabelaPrecoId", "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\r\n                    FROM TGFPAR (NOLOCK) PAR\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND\r\n                                            AND VEN.CODVEND = $VendedorId \r\n                                            AND VEN.TIPVEND = 'R'\r\n                    WHERE PAR.CLIENTE = 'S' \r\n                    AND PAR.CODPARC > 0 \r\n                    AND PAR.CODVEND > 0\r\n                    AND PAR.ATIVO = 'S'", "TabelaPrecoParceiro" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Ordem" },
                values: new object[,]
                {
                    { 1, 1, "fa fa-bank", "Administrativo", 0 },
                    { 2, 4, "fa fa-address-card", "Cadastros", 0 },
                    { 3, 5, "fa fa-map-o", "Movimentos", 0 },
                    { 4, 7, "fa fa-search-minus", "Consultas", 0 },
                    { 5, 6, "fa fa-object-ungroup", "Outros", 0 },
                    { 10, 25, "fa fa-cogs", "Configurações", 0 }
                });

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
                    { 21, 21, "fa fa-id-card-o", "Cadastro de Páginas", "/cadastro-de-paginas" },
                    { 22, 22, "fa fa-newspaper-o", "Montar Menu", "/montar-menu" },
                    { 23, 23, "fa fa-line-chart", "Pedido de Vendas", "/pedido_vendas" },
                    { 24, 24, "fa fa-external-link-square", "Receber dados Sankhya", "" }
                });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 17, 13, "fa fa-briefcase", 2, "Empresas", null, "/cadastro-tipo-empresa" },
                    { 18, 13, "fa fa-user-plus", 2, "Vendedores", null, "/cadastro-vendedores" },
                    { 19, 18, "fa fa-credit-card", 2, "Tipo de Negociação", null, "/cadastro-tipo-negociacao" },
                    { 20, 14, "fa fa-users", 2, "Parceiros", null, "/cadastro-parceiros" },
                    { 21, 12, "fa fa-shopping-bag", 2, "Grupo de Produtos", null, "/cadastro-grupos-produtos" },
                    { 22, 11, "fa fa-cart-plus", 2, "Produtos", null, "/cadastro-produtos" },
                    { 23, 16, "fa fa-user-times", 2, "Concorrentes", null, "/cadastro-concorrentes" },
                    { 24, 17, "fa fa-user-times", 2, "Produto x Concorrente", null, "/produtos-concorrentes" },
                    { 25, 19, "fa fa-calculator", 2, "Tabela de Preço", null, "/tabela-de-preco" },
                    { 26, 23, "fa fa-line-chart", 3, "Pedido de Vendas", null, "/pedido_vendas" },
                    { 27, 9, "fa fa-user-circle-o", 5, "Usuarios", null, "/cadastro-usuarios" },
                    { 28, 10, "fa fa-users", 5, "Grupo de Usuarios", null, "/cadastro-grupo-usuarios" },
                    { 31, 24, "fa fa-external-link-square", 5, "Receber dados Sankhya", null, "" },
                    { 32, 26, "fa fa-refresh", 10, "Restaurar dados sistema", null, "" },
                    { 33, 27, "fa fa-line-chart", 3, "Dashboard", null, "/dashboard" },
                    { 36, 28, "fa fa-cogs", 10, "Configurações Avançadas", null, "/configuracoes" }
                });

            migrationBuilder.InsertData(
                table: "SubMenu",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "Ordem" },
                values: new object[,]
                {
                    { 1, 4, "fa fa-address-card", 1, "Cadastros", 0 },
                    { 2, 5, "fa fa-map-o", 1, "Movimentos", 0 },
                    { 3, 7, "fa fa-search-minus", 1, "Consultas", 0 },
                    { 4, 6, "fa fa-object-ungroup", 1, "Outros", 0 },
                    { 10, 25, "fa fa-cogs", 1, "Configurações", 0 }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "Funcao", "GrupoId", "ImagemURL", "NomeCompleto", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PrimeiroLoginAdm", "RefreshToken", "ResetTokenExpires", "Status", "Telefone", "TokenCreated", "TokenExpires", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { 1, "nfe@grupoalyne.com.br", "Administrador do Sistema", 1, "", "Administrador Grupo Alyne", new byte[] { 45, 81, 111, 37, 67, 74, 128, 26, 119, 74, 48, 209, 244, 126, 138, 236, 147, 144, 127, 238, 49, 11, 208, 206, 250, 171, 153, 236, 37, 161, 101, 173, 42, 119, 75, 214, 230, 103, 88, 175, 96, 12, 101, 48, 81, 109, 14, 43, 51, 234, 241, 23, 218, 141, 195, 234, 192, 142, 55, 173, 65, 197, 54, 177 }, null, new byte[] { 27, 67, 0, 98, 161, 61, 212, 145, 30, 80, 57, 30, 245, 150, 199, 15, 154, 71, 41, 242, 25, 44, 239, 229, 109, 12, 238, 214, 233, 22, 178, 245 }, true, "", null, "1", "(85) 3521-8888", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", null, null });

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
                    { 11, 23, "fa fa-line-chart", 1, "Pedido de Vendas", 2, "/pedido_vendas" },
                    { 12, 9, "fa fa-user-circle-o", 1, "Usuarios", 4, "/cadastro-usuarios" },
                    { 13, 10, "fa fa-users", 1, "Grupo de Usuarios", 4, "/cadastro-grupo-usuarios" },
                    { 16, 24, "fa fa-external-link-square", 1, "Receber dados Sankhya", 4, "" },
                    { 29, 26, "fa fa-refresh", 1, "Restaurar dados sistema", 10, "" },
                    { 34, 27, "fa fa-line-chart", 1, "Dashboard", 2, "/dashboard" },
                    { 35, 28, "fa fa-cogs", 1, "Configurações Avançadas", 10, "/configuracoes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabecalhoPedidoVenda_ParceiroId",
                table: "CabecalhoPedidoVenda",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_CabecalhoPedidoVenda_TipoNegociacaoId",
                table: "CabecalhoPedidoVenda",
                column: "TipoNegociacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CabecalhoPedidoVenda_VendedorId",
                table: "CabecalhoPedidoVenda",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidoVenda_CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda",
                column: "CabecalhoPedidoVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidoVenda_ProdutoId",
                table: "ItemPedidoVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidoVenda_VendedorId",
                table: "ItemPedidoVenda",
                column: "VendedorId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_ParceiroId",
                table: "Titulo",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GrupoId",
                table: "Usuario",
                column: "GrupoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concorrente");

            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "IntegracaoSankhya");

            migrationBuilder.DropTable(
                name: "ItemPedidoVenda");

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
                name: "TabelaPrecoParceiro");

            migrationBuilder.DropTable(
                name: "Titulo");

            migrationBuilder.DropTable(
                name: "CabecalhoPedidoVenda");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "SubMenuPermissao");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "TabelaPreco");

            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "TipoNegociacao");

            migrationBuilder.DropTable(
                name: "GrupoProduto");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuPermissao");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");
        }
    }
}
