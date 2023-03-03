
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortalGrupoAlyne.Model.Dtos.Usuarios;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PortalGrupoAlyne.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
       
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //optionsBuilder
        //    .UseMySql("server=localhost;database=GrupoAlyne;user=root;password=root");
    }
    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<GrupoProduto> GrupoProduto { get; set; }
   
    public DbSet<Vendedor> Vendedor { get; set; }

    public DbSet<Produto> Produto { get; set; }

    public DbSet<Concorrente> Concorrente { get; set; }

    public DbSet<ProdutoConcorrente> ProdutoConcorrente { get; set; }

    public DbSet<Parceiro> Parceiro { get; set;}

    public DbSet<TipoNegociacao> TipoNegociacao { get;set; }

    public DbSet<TabelaPreco> TabelaPreco { get;set; }

    public DbSet<ItemTabela> ItemTabela { get; set; }
    public DbSet<Empresa> Empresa { get; set; }



    public DbSet<Pagina> Pagina { get; set; }
    public DbSet<PaginaBase> PaginaBase { get; set; }
    
    public DbSet<Menu> Menu { get; set; }

    public DbSet<SubMenu> SubMenu { get; set; }

    public DbSet<GrupoUsuario> GrupoUsuario { get; set;}

    public DbSet<MenuPermissao> MenuPermissao { get; set; }

    public DbSet<SubMenuPermissao> SubMenuPermissao { get; set; }

    public DbSet<PaginaPermissao> PaginaPermissao { get; set;}

    public DbSet<TabelaPrecoParceiro> TabelaPrecoParceiro { get; set; }

    public DbSet<CabecalhoPedidoVenda> CabecalhoPedidoVenda { get; set; }

    public DbSet<ItemPedidoVenda> ItemPedidoVenda { get; set; }
    
    public DbSet<Configuracao> Configuracao { get; set; }

    public DbSet<IntegracaoSankhya> IntegracaoSankhya { get; set; }

        //private byte[] CreateHash(string input)
        //{
        //    using var sha256 = SHA256.Create();
        //    var inputBytes = Encoding.UTF8.GetBytes(input);
        //    var hashBytes = sha256.ComputeHash(inputBytes);
        //    return hashBytes;
        //}

        //private byte[] CreateSalt()
        //{
        //    using var rng = new RNGCryptoServiceProvider();
        //    var salt = new byte[32];
        //    rng.GetBytes(salt);
        //    return salt;
        //}
        private byte[] CreateHash(string input, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = hmac.ComputeHash(inputBytes);
            return hashBytes;
        }

        private byte[] CreateSalt()
        {
            using var rng = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            rng.GetBytes(salt);
            return salt;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var password = "Sync550v";
            var salt = CreateSalt();

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "nfe@grupoalyne.com.br",
                    Username = "admin",
                    NomeCompleto = "Administrador Grupo Alyne",
                    PasswordHash = CreateHash(password, salt),
                    PasswordSalt = salt,
                    Status = "1",
                    GrupoId = 1,
                    Funcao = "Administrador do Sistema",
                    Telefone = "(85) 3521-8888",
                    ImagemURL = "",
                    PrimeiroLoginAdm = true
                }
            );
            

            modelBuilder.Entity<Empresa>().HasData(
               new Empresa
               {
                   Id = 1,
                   Descricao = "Industria"
                   
               },
               new Empresa
               {
                   Id = 2,
                   Descricao = "Distribuidora"

               }
           );
            modelBuilder.Entity<PaginaBase>().HasData(
              new PaginaBase
              {
                  Id = 1,
                 Codigo = 1,
                 Nome = "Administrativo",
                 Url = "",
                 Icon = "fa fa-bank"
              },
              new PaginaBase
              {
                  Id = 2,
                  Codigo = 2,
                  Nome = "Trade",
                  Url = "",
                  Icon = "fa fa-bar-chart"
              },
              new PaginaBase
              {
                  Id = 3,
                  Codigo = 3,
                  Nome = "Vendas",
                  Url = "",
                  Icon = "fa fa-money"
              },
               new PaginaBase
               {
                   Id = 4,
                   Codigo = 4,
                   Nome = "Cadastros",
                   Url = "",
                   Icon = "fa fa-address-card"
               },
                new PaginaBase
                {
                    Id = 5,
                    Codigo = 5,
                    Nome = "Movimentos",
                    Url = "",
                    Icon = "fa fa-map-o"
                },
                new PaginaBase
                {
                    Id = 6,
                    Codigo = 6,
                    Nome = "Outros",
                    Url = "",
                    Icon = "fa fa-object-ungroup"
                },
                new PaginaBase
                {
                    Id = 7,
                    Codigo = 7,
                    Nome = "Consultas",
                    Url = "",
                    Icon = "fa fa-search-minus"
                },
                 new PaginaBase
                 {
                     Id = 8,
                     Codigo = 8,
                     Nome = "Relatorios",
                     Url = "",
                     Icon = "fa fa-file-o"
                 },
               new PaginaBase
               {
                   Id = 9,
                   Codigo = 9,
                   Nome = "Usuarios",
                   Url = "/cadastro-usuarios",
                   Icon = "fa fa-user-circle-o"
               },
                new PaginaBase
                {
                    Id = 10,
                    Codigo = 10,
                    Nome = "Grupo de Usuarios",
                    Url = "/cadastro-grupo-usuarios",
                    Icon = "fa fa-users"
                },
                 new PaginaBase
                 {
                     Id = 11,
                     Codigo = 11,
                     Nome = "Produtos",
                     Url = "/cadastro-produtos",
                     Icon = "fa fa-cart-plus"
                 },
                 new PaginaBase
                 {
                     Id = 12,
                     Codigo = 12,
                     Nome = "Grupo de Produtos",
                     Url = "/cadastro-grupos-produtos",
                     Icon = "fa fa-shopping-bag"
                 },
                 new PaginaBase
                 {
                     Id = 13,
                     Codigo = 13,
                     Nome = "Vendedores",
                     Url = "/cadastro-vendedores",
                     Icon = "fa fa-user-plus"
                 },
               new PaginaBase
               {
                   Id = 14,
                   Codigo = 14,
                   Nome = "Parceiros",
                   Url = "/cadastro-parceiros",
                   Icon = "fa fa-users"
               },
                 new PaginaBase
                 {
                     Id = 15,
                     Codigo = 15,
                     Nome = "Empresas",
                     Url = "/cadastro-tipo-empresa",
                     Icon = "fa fa-briefcase"
                 },
                 new PaginaBase
                 {
                     Id = 16,
                     Codigo = 16,
                     Nome = "Concorrentes",
                     Url = "/cadastro-concorrentes",
                     Icon = "fa fa-user-times"
                 },
                 new PaginaBase
                 {
                     Id = 17,
                     Codigo = 17,
                     Nome = "Produto x Concorrente",
                     Url = "/produtos-concorrentes",
                     Icon = "fa fa-user-times"
                 },
                 new PaginaBase
                 {
                     Id = 18,
                     Codigo = 18,
                     Nome = "Tipo de Negociação",
                     Url = "/cadastro-tipo-negociacao",
                     Icon = "fa fa-credit-card"
                 },
                 new PaginaBase
                 {
                     Id = 19,
                     Codigo = 19,
                     Nome = "Tabela de Preço",
                     Url = "/tabela-de-preco",
                     Icon = "fa fa-calculator"
                 },
                 
                 new PaginaBase
                 {
                     Id = 21,
                     Codigo = 21,
                     Nome = "Cadastro de Páginas",
                     Url = "/cadastro-de-paginas",
                     Icon = "fa fa-id-card-o"
                 },
                  new PaginaBase
                  {
                      Id = 22,
                      Codigo = 22,
                      Nome = "Montar Menu",
                      Url = "/montar-menu",
                      Icon = "fa fa-newspaper-o"
                  },
                  new PaginaBase
                  {
                      Id = 23,
                      Codigo = 23,
                      Nome = "Pedido de Vendas",
                      Url = "/pedido_vendas",
                      Icon = "fa fa-line-chart"

                  },
                   new PaginaBase
                   {
                       Id = 24,
                       Codigo = 24,
                       Nome = "Receber dados Sankhya",
                       Url = "",
                       Icon = "fa fa-external-link-square"

                   }

          );

            modelBuilder.Entity<Menu>().HasData(
              new Menu
              {
                  Id = 1,
                  Codigo = 1,
                  Ordem = 0,
                  Nome = "Administrativo",
                  Icon = "fa fa-bank",

              },
               new Menu
               {
                   Id = 2,
                   Codigo = 4,
                   Ordem = 0,
                   Nome = "Cadastros",
                   Icon = "fa fa-address-card",

               },
               new Menu
               {
                   Id = 3,
                   Codigo = 5,
                   Ordem = 0,
                   Nome = "Movimentos",
                   Icon = "fa fa-map-o",

               }, 
               new Menu
               {
                   Id = 4,
                   Codigo = 7,
                   Ordem = 0,
                   Nome = "Consultas",
                   Icon = "fa fa-search-minus",

               },
                new Menu
                {
                    Id = 5,
                    Codigo = 6,
                    Ordem = 0,
                    Nome = "Outros",
                    Icon = "fa fa-object-ungroup",

                },
                new Menu
                { 
                    Id = 10, 
                    Codigo = 25, 
                    Ordem = 0, 
                    Nome = "Configurações", 
                    Icon = "fa fa-cogs" 
                }
          );

            modelBuilder.Entity<SubMenu>().HasData(
             new SubMenu
             {
                 Id = 1,
                 Codigo = 4,
                 Ordem = 0,
                 Nome = "Cadastros",
                 Icon = "fa fa-address-card",
                 MenuId= 1
             },
              new SubMenu
              {
                  Id = 2,
                  Codigo = 5,
                  Ordem = 0,
                  Nome = "Movimentos",
                  Icon = "fa fa-map-o",
                  MenuId = 1
              },
               new SubMenu
               {
                   Id = 3,
                   Codigo = 7,
                   Ordem = 0,
                   Nome = "Consultas",
                   Icon = "fa fa-search-minus",
                   MenuId = 1
               },
               new SubMenu
               {
                   Id = 4,
                   Codigo = 6,
                   Ordem = 0,
                   Nome = "Outros",
                   Icon = "fa fa-object-ungroup",
                   MenuId = 1
               },
               new SubMenu
               {
                   Id = 10,
                   Codigo = 25,
                   Ordem = 0,
                   Nome = "Configurações",
                   Icon = "fa fa-cogs",
                   MenuId = 1
               }
         );
            modelBuilder.Entity<Pagina>().HasData(
                 new Pagina
                 {
                     Id = 1,
                     Codigo = 13,
                     Nome = "Empresas",
                     Url = "/cadastro-tipo-empresa",
                     Icon = "fa fa-briefcase",
                     MenuId = 1,
                     SubMenuId=1,


                 },
                  new Pagina
                  {
                      Id = 2,
                      Codigo = 13,
                      Nome = "Vendedores",
                      Url = "/cadastro-vendedores",
                      Icon = "fa fa-user-plus",
                      MenuId = 1,
                      SubMenuId = 1,

                  }, new Pagina
                  {
                      Id = 3,
                      Codigo = 18,
                      Nome = "Tipo de Negociação",
                      Url = "/cadastro-tipo-negociacao",
                      Icon = "fa fa-credit-card",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                  new Pagina
                  {
                      Id = 4,
                      Codigo = 14,
                      Nome = "Parceiros",
                      Url = "/cadastro-parceiros",
                      Icon = "fa fa-users",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                  new Pagina
                  {
                      Id = 5,
                      Codigo = 12,
                      Nome = "Grupo de Produtos",
                      Url = "/cadastro-grupos-produtos",
                      Icon = "fa fa-shopping-bag",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                  new Pagina
                  {
                      Id = 6,
                      Codigo = 11,
                      Nome = "Produtos",
                      Url = "/cadastro-produtos",
                      Icon = "fa fa-cart-plus",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                  new Pagina
                  {
                      Id = 7,
                      Codigo = 16,
                      Nome = "Concorrentes",
                      Url = "/cadastro-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                  new Pagina
                  {
                      Id = 8,
                      Codigo = 17,
                      Nome = "Produto x Concorrente",
                      Url = "/produtos-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 1,
                      SubMenuId = 1,
                  }, 
                  new Pagina
                  {
                      Id = 9,
                      Codigo = 19,
                      Nome = "Tabela de Preço",
                      Url = "/tabela-de-preco",
                      Icon = "fa fa-calculator",
                      MenuId = 1,
                      SubMenuId = 1,
                  },
                
                  new Pagina
                  {
                      Id = 11,
                      Codigo = 23,
                      Nome = "Pedido de Vendas",
                      Url = "/pedido_vendas",
                      Icon = "fa fa-line-chart",
                      MenuId = 1,
                      SubMenuId = 2,
                  },
              new Pagina
              {
                  Id = 12,
                  Codigo = 9,
                  Nome = "Usuarios",
                  Url = "/cadastro-usuarios",
                  Icon = "fa fa-user-circle-o",
                  MenuId = 1,
                  SubMenuId = 4,
              },
               new Pagina
               {
                   Id = 13,
                   Codigo = 10,
                   Nome = "Grupo de Usuarios",
                   Url = "/cadastro-grupo-usuarios",
                   Icon = "fa fa-users",
                   MenuId = 1,
                   SubMenuId = 4,
               },
                 
                  new Pagina
                  {
                      Id = 16,
                      Codigo = 24,
                      Nome = "Receber dados Sankhya",
                      Url = "",
                      Icon = "fa fa-external-link-square",
                      MenuId = 1,
                      SubMenuId = 4,

                  },
                    new Pagina
                    {
                        Id = 29,
                        Codigo = 26,
                        Nome = "Restaurar dados sistema",
                        Url = "",
                        Icon = "fa fa-refresh",
                        MenuId = 1,
                        SubMenuId = 10,
                    },
                  new Pagina
                  {
                      Id = 17,
                      Codigo = 13,
                      Nome = "Empresas",
                      Url = "/cadastro-tipo-empresa",
                      Icon = "fa fa-briefcase",
                      MenuId = 2,


                  },
                  new Pagina
                  {
                      Id = 18,
                      Codigo = 13,
                      Nome = "Vendedores",
                      Url = "/cadastro-vendedores",
                      Icon = "fa fa-user-plus",
                      MenuId = 2,
                      

                  }, new Pagina
                  {
                      Id = 19,
                      Codigo = 18,
                      Nome = "Tipo de Negociação",
                      Url = "/cadastro-tipo-negociacao",
                      Icon = "fa fa-credit-card",
                      MenuId = 2,
                      
                  },
                  new Pagina
                  {
                      Id = 20,
                      Codigo = 14,
                      Nome = "Parceiros",
                      Url = "/cadastro-parceiros",
                      Icon = "fa fa-users",
                      MenuId = 2,
                  },
                  new Pagina
                  {
                      Id = 21,
                      Codigo = 12,
                      Nome = "Grupo de Produtos",
                      Url = "/cadastro-grupos-produtos",
                      Icon = "fa fa-shopping-bag",
                      MenuId = 2,
                  },
                  new Pagina
                  {
                      Id = 22,
                      Codigo = 11,
                      Nome = "Produtos",
                      Url = "/cadastro-produtos",
                      Icon = "fa fa-cart-plus",
                      MenuId = 2,
                  },
                  new Pagina
                  {
                      Id = 23,
                      Codigo = 16,
                      Nome = "Concorrentes",
                      Url = "/cadastro-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 2,

                  },
                  new Pagina
                  {
                      Id = 24,
                      Codigo = 17,
                      Nome = "Produto x Concorrente",
                      Url = "/produtos-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 2,
                  },
                  new Pagina
                  {
                      Id = 25,
                      Codigo = 19,
                      Nome = "Tabela de Preço",
                      Url = "/tabela-de-preco",
                      Icon = "fa fa-calculator",
                      MenuId = 2,
                  },

                  new Pagina
                  {
                      Id = 26,
                      Codigo = 23,
                      Nome = "Pedido de Vendas",
                      Url = "/pedido_vendas",
                      Icon = "fa fa-line-chart",
                      MenuId = 3,
                      
                  },
              new Pagina
              {
                  Id = 27,
                  Codigo = 9,
                  Nome = "Usuarios",
                  Url = "/cadastro-usuarios",
                  Icon = "fa fa-user-circle-o",
                  MenuId = 5,
                  
              },
               new Pagina
               {
                   Id = 28,
                   Codigo = 10,
                   Nome = "Grupo de Usuarios",
                   Url = "/cadastro-grupo-usuarios",
                   Icon = "fa fa-users",
                   MenuId = 5,
                   
               },
                 
                  new Pagina
                  {
                      Id = 31,
                      Codigo = 24,
                      Nome = "Receber dados Sankhya",
                      Url = "",
                      Icon = "fa fa-external-link-square",
                      MenuId = 5,
                     

                  },
                  new Pagina
                  {
                      Id = 32,
                      Codigo = 26,
                      Nome = "Restaurar dados sistema",
                      Url = "",
                      Icon = "fa fa-refresh",
                      MenuId = 10,


                  }

          );
            modelBuilder.Entity<GrupoUsuario>().HasData(
               new GrupoUsuario
               {
                   Id = 1,
                   Nome = "Administrativo"

               }
           );

            modelBuilder.Entity<Configuracao>().HasData(
                 new Configuracao
                 {
                     Id = 1,
                     SankhyaServidor = "http://10.0.0.254:8280/",
                     SankhyaUsuario = "ADMIN",
                     SankhyaSenha = "SYNC550V"
                 }
             );
            modelBuilder.Entity<IntegracaoSankhya>().HasData(
               new IntegracaoSankhya
               {
                   Id = 1,
                   TabelaPortal = "Vendedor",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, 
                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm
                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId AND DTALTER > '$AtualizadoEm'"
               },
               new IntegracaoSankhya
               {
                   Id = 2,
                   TabelaPortal = "TipoNegociacao",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT DISTINCT TPV.CODTIPVENDA Id, 
                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,
                        TPV.DHALTER AtualizadoEm
                    FROM TGFTPV (NOLOCK) TPV
                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA
                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'
                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		
                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' 
                                            AND VEN.CODVEND = $VendedorId
                    WHERE TPV.CODTIPVENDA > 0
                    AND DHALTER > '$AtualizadoEm'
                    ORDER BY 1"
               },
               new IntegracaoSankhya
               {
                   Id = 3,
                   TabelaPortal = "Parceiro",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, 
                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, 
                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, 
                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, 
                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,
                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,
                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, 
                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, 
                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm
                    FROM TGFPAR (NOLOCK) PAR
					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' 
                                            AND VEN.CODVEND = $VendedorId
                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID
                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF
                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC
                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND
                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI
                    WHERE PAR.CLIENTE = 'S' 
                    AND PAR.CODPARC > 0 
                    AND PAR.CODVEND > 0
                    AND PAR.ATIVO = 'S'"
               },
               new IntegracaoSankhya
               {
                   Id = 4,
                   TabelaPortal = "GrupoProduto",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) Id, 
                    RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome
                    FROM sankhya.TGFGRU (NOLOCK)
                    WHERE ANALITICO = 'S'
                    and SUBSTRING(RTRIM(CODGRUPOPROD),1,3) = '120'"
               },
               new IntegracaoSankhya
               {
                   Id = 5,
                   TabelaPortal = "Produto",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT PRO.CODPROD Id, 
                        PRO.DESCRPROD Nome, 
                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,
                        PRO.DTALTER AtualizadoEm,
                        PRO.CODVOL TipoUnid,
                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,
                        ISNULL(VOA.QUANTIDADE,1) Conv
                    FROM sankhya.TGFPRO (NOLOCK) PRO
                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'
                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'
                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')
                    AND PRO.DTALTER > '$AtualizadoEm'"
               },
               new IntegracaoSankhya
               {
                   Id = 6,
                   TabelaPortal = "TabelaPreco",
                   ChaveTabelaPortal = "Id",
                   SqlObterSankhya = @"SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal 
                    FROM TGFNTA (NOLOCK) NTA
                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB
                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB
                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC
                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND 
                                            AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R'
                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR 
                    ORDER BY 1"
               },
               new IntegracaoSankhya
               {
                   Id = 7,
                   TabelaPortal = "ItemTabela",
                   ChaveTabelaPortal = "TabelaPrecoId,IdProd",
                   SqlObterSankhya = @"SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, 
                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm
                    FROM TGFTAB TAB
                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB
                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB
                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD
                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB 
                                            FROM TGFNTA (NOLOCK) NTA
                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB
                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC
						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND 
                                                                    AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R' 
                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))
                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB
                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())
                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)
                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'
                    ORDER BY TAB.CODTAB, PRO.CODPROD"
               },
               new IntegracaoSankhya
               {
                   Id = 8,
                   TabelaPortal = "TabelaPrecoParceiro",
                   ChaveTabelaPortal = "ParceiroId,EmpresaId,TabelaPrecoId",
                   SqlObterSankhya = @"SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId
                    FROM TGFPAR (NOLOCK) PAR
                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC
                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND
                                            AND VEN.CODVEND = $VendedorId 
                                            AND VEN.TIPVEND = 'R'
                    WHERE PAR.CLIENTE = 'S' 
                    AND PAR.CODPARC > 0 
                    AND PAR.CODVEND > 0
                    AND PAR.ATIVO = 'S'"

               }

           );

        }


    }
}
