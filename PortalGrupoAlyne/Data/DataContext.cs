
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortalGrupoAlyne.Model.Dtos.Usuarios;
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

    public DbSet<TabelaPrecoCliente> TabelaPrecoCliente { get; set; }

    public DbSet<Pagina> Pagina { get; set; }
    public DbSet<PaginaBase> PaginaBase { get; set; }
    
    public DbSet<Menu> Menu { get; set; }

    public DbSet<SubMenu> SubMenu { get; set; }

    public DbSet<GrupoUsuario> GrupoUsuario { get; set;}

    public DbSet<MenuPermissao> MenuPermissao { get; set; }

    public DbSet<SubMenuPermissao> SubMenuPermissao { get; set; }

    public DbSet<PaginaPermissao> PaginaPermissao { get; set;}

    public DbSet<TabelaPrecoParceiro> TabelaPrecoParceiro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "nfe@grupoalyne.com.br",
                    Username = "admin",
                    NomeCompleto = "Administrador Grupo Alyne",
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
                   Descricao = "Indústria"
                   
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
                     Id = 14,
                     Codigo = 22,
                     Nome = "Montar Menu",
                     Url = "/montar-menu",
                     Icon = "fa fa-newspaper-o",
                     MenuId = 1,
                     SubMenuId = 4,

                 },
                  new Pagina
                  {
                      Id = 15,
                      Codigo = 21,
                      Nome = "Cadastro de Páginas",
                      Url = "/cadastro-de-paginas",
                      Icon = "fa fa-id-card-o",
                      MenuId = 1,
                      SubMenuId = 4,

                  }

          );
            modelBuilder.Entity<GrupoUsuario>().HasData(
               new GrupoUsuario
               {
                   Id = 1,
                   Nome = "Administrativo"

               }
           );
         
        }


    }
}
