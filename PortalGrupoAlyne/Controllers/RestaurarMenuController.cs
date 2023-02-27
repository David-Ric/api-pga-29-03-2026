using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Services;
using SQLitePCL;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using Dapper;
using MySqlConnector;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurarMenuController : ControllerBase
    {
        private readonly DataContext _context;
        private IPaginaService _paginaService;
        public RestaurarMenuController(DataContext context, IPaginaService paginaService)
        {
            _paginaService = paginaService;
            _context = context;
        }
        

        [HttpPost]
        public async Task<ActionResult> DeleteAllPages([FromServices] DataContext context)
        {
            try
            {
                var empresas = await context.Empresa.AsNoTracking().OrderBy(e => e.Id).ToListAsync();

                var empresa1 = await _context.Empresa
                .FindAsync(1);
                if (empresa1 == null)
                {
                    var novaEmpresa = new List<Empresa> {
                new Empresa { Id = 1, Descricao = "Industria"} };
                    _context.Empresa.AddRange(novaEmpresa);
                }
                var empresa2 = await _context.Empresa
                .FindAsync(2);
                if (empresa2 == null)
                {
                    var novaEmpresa2 = new List<Empresa> {
                new Empresa { Id = 2, Descricao = "Distribuidora"} };
                    _context.Empresa.AddRange(novaEmpresa2);
                }
                var empresa3 = await _context.Empresa
                .FindAsync(3);

                var grupoUsuario = await _context.GrupoUsuario
                .FindAsync(1);
                if (grupoUsuario == null)
                {
                    var novoUsuario = new List<GrupoUsuario> {
                new GrupoUsuario { Id = 1, Nome = "Administrativo"} };
                    _context.GrupoUsuario.AddRange(novoUsuario);
                }
                var usuario1 = await _context.Usuario
                .FindAsync(1);
                if (usuario1 == null)
                {
                    var novoUser= new List<Usuario> {
                new Usuario {
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


                } };
                    _context.Usuario.AddRange(novoUser);
                }



                var paginas = await context.Pagina.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
                if (paginas == null) return NoContent();
                _context.Pagina.RemoveRange(paginas);
                var submenu = await context.SubMenu.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
                if (submenu == null) return NoContent();
                _context.SubMenu.RemoveRange(submenu);
                var menu = await context.Menu.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
                if (menu == null) return NoContent();
                _context.Menu.RemoveRange(menu);

                var novoMenu = new List<Menu> { 
                new Menu { Id = 1, Codigo = 1, Ordem = 0, Nome = "Administrativo", Icon = "fa fa-bank" },
                new Menu { Id = 2, Codigo = 4, Ordem = 0, Nome = "Cadastros", Icon = "fa fa-address-card" },
                new Menu { Id = 3, Codigo = 5, Ordem = 0, Nome = "Movimentos", Icon = "fa fa-map-o" },
                new Menu { Id = 4, Codigo = 7, Ordem = 0, Nome = "Consultas", Icon = "fa fa-search-minus" },
                new Menu { Id = 5, Codigo = 6, Ordem = 0, Nome = "Outros", Icon = "fa fa-object-ungroup" },
                new Menu { Id = 6, Codigo = 25, Ordem = 0, Nome = "Configurações",Icon = "fa fa-cogs" }

                };
                _context.Menu.AddRange(novoMenu);

                var novoSubMenu = new List<SubMenu> { 
                    new SubMenu { Id = 1, Codigo = 4, Ordem = 0, Nome = "Cadastros", Icon = "fa fa-address-card",MenuId=1  },
                    new SubMenu { Id = 2, Codigo = 3, Ordem = 0,  Nome = "Movimentos", Icon = "fa fa-map-o",MenuId=1  },
                    new SubMenu { Id = 3, Codigo = 7, Ordem = 0, Nome = "Consultas", Icon = "fa fa-search-minus",MenuId=1  },
                    new SubMenu { Id = 4, Codigo = 6, Ordem = 0, Nome = "Outros", Icon = "fa fa-object-ungroup",MenuId=1  },
                    new SubMenu {  Id = 5,Codigo = 25,Ordem = 0,Nome = "Configurações",Icon = "fa fa-cogs",MenuId = 1}

                };
                _context.SubMenu.AddRange(novoSubMenu);

                var novaPagina = new List<Pagina>
                { 
                    new Pagina {
                     Id = 1,
                     Codigo = 13,
                     Nome = "Empresas",
                     Url = "/cadastro-tipo-empresa",
                     Icon = "fa fa-briefcase",
                     MenuId = 1,
                     SubMenuId=1,
                },
                new Pagina {
                      Id = 2,
                      Codigo = 13,
                      Nome = "Vendedores",
                      Url = "/cadastro-vendedores",
                      Icon = "fa fa-user-plus",
                      MenuId = 1,
                      SubMenuId = 1,
               },


                     new Pagina {
                      Id = 3,
                      Codigo = 18,
                      Nome = "Tipo de Negociação",
                      Url = "/cadastro-tipo-negociacao",
                      Icon = "fa fa-credit-card",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                      new Pagina {
                      Id = 4,
                      Codigo = 14,
                      Nome = "Parceiros",
                      Url = "/cadastro-parceiros",
                      Icon = "fa fa-users",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                       new Pagina {
                      Id = 5,
                      Codigo = 12,
                      Nome = "Grupo de Produtos",
                      Url = "/cadastro-grupos-produtos",
                      Icon = "fa fa-shopping-bag",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                       new Pagina {
                      Id = 6,
                      Codigo = 11,
                      Nome = "Produtos",
                      Url = "/cadastro-produtos",
                      Icon = "fa fa-cart-plus",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                       new Pagina {
                      Id = 7,
                      Codigo = 16,
                      Nome = "Concorrentes",
                      Url = "/cadastro-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                        new Pagina {
                      Id = 8,
                      Codigo = 17,
                      Nome = "Produto x Concorrente",
                      Url = "/produtos-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                        new Pagina {
                      Id = 9,
                      Codigo = 19,
                      Nome = "Tabela de Preço",
                      Url = "/tabela-de-preco",
                      Icon = "fa fa-calculator",
                      MenuId = 1,
                      SubMenuId = 1,
                     },
                         new Pagina {
                      Id = 11,
                      Codigo = 23,
                      Nome = "Pedido de Vendas",
                      Url = "/pedido_vendas",
                      Icon = "fa fa-line-chart",
                      MenuId = 1,
                      SubMenuId = 2,
                     },
                      new Pagina {
                    Id = 12,
                    Codigo = 9,
                    Nome = "Usuarios",
                    Url = "/cadastro-usuarios",
                    Icon = "fa fa-user-circle-o",
                    MenuId = 1,
                    SubMenuId = 4,
                     },
                        new Pagina {
                      Id = 13,
                      Codigo = 10,
                      Nome = "Grupo de Usuarios",
                      Url = "/cadastro-grupo-usuarios",
                      Icon = "fa fa-users",
                      MenuId = 1,
                      SubMenuId = 4,
                     },
                         new Pagina {
                      Id = 16,
                      Codigo = 24,
                      Nome = "Receber dados Sankhya",
                      Url = "",
                      Icon = "fa fa-external-link-square",
                      MenuId = 1,
                      SubMenuId = 4,
                     },
                          new Pagina {
                      Id = 29,
                        Codigo = 26,
                        Nome = "Restaurar dados sistema",
                        Url = "",
                        Icon = "fa fa-refresh",
                        MenuId = 1,
                        SubMenuId = 5,
                     },
                         new Pagina {
                      Id = 17,
                      Codigo = 13,
                      Nome = "Empresas",
                      Url = "/cadastro-tipo-empresa",
                      Icon = "fa fa-briefcase",
                      MenuId = 2,
                     },
                         new Pagina {
                      Id = 18,
                      Codigo = 13,
                      Nome = "Vendedores",
                      Url = "/cadastro-vendedores",
                      Icon = "fa fa-user-plus",
                      MenuId = 2,
                     },
                         new Pagina {
                      Id = 19,
                      Codigo = 18,
                      Nome = "Tipo de Negociação",
                      Url = "/cadastro-tipo-negociacao",
                      Icon = "fa fa-credit-card",
                      MenuId = 2,
                     },
                         new Pagina {
                      Id = 20,
                      Codigo = 14,
                      Nome = "Parceiros",
                      Url = "/cadastro-parceiros",
                      Icon = "fa fa-users",
                      MenuId = 2,
                     },
                          new Pagina {
                      Id = 21,
                      Codigo = 12,
                      Nome = "Grupo de Produtos",
                      Url = "/cadastro-grupos-produtos",
                      Icon = "fa fa-shopping-bag",
                      MenuId = 2,
                     },
                          new Pagina {
                      Id = 22,
                      Codigo = 11,
                      Nome = "Produtos",
                      Url = "/cadastro-produtos",
                      Icon = "fa fa-cart-plus",
                      MenuId = 2,
                     },
                          new Pagina {
                      Id = 23,
                      Codigo = 16,
                      Nome = "Concorrentes",
                      Url = "/cadastro-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 2,
                     },
                           new Pagina {
                       Id = 24,
                      Codigo = 17,
                      Nome = "Produto x Concorrente",
                      Url = "/produtos-concorrentes",
                      Icon = "fa fa-user-times",
                      MenuId = 2,
                     },
                           new Pagina {
                       Id = 25,
                      Codigo = 19,
                      Nome = "Tabela de Preço",
                      Url = "/tabela-de-preco",
                      Icon = "fa fa-calculator",
                      MenuId = 2,
                     },
                           new Pagina {
                       Id = 26,
                      Codigo = 23,
                      Nome = "Pedido de Vendas",
                      Url = "/pedido_vendas",
                      Icon = "fa fa-line-chart",
                      MenuId = 3,
                     },
                           new Pagina {
                       Id = 27,
                  Codigo = 9,
                  Nome = "Usuarios",
                  Url = "/cadastro-usuarios",
                  Icon = "fa fa-user-circle-o",
                  MenuId = 5,
                     },
                           new Pagina {
                        Id = 28,
                   Codigo = 10,
                   Nome = "Grupo de Usuarios",
                   Url = "/cadastro-grupo-usuarios",
                   Icon = "fa fa-users",
                   MenuId = 5,
                     },
                           new Pagina {
                        Id = 31,
                      Codigo = 24,
                      Nome = "Receber dados Sankhya",
                      Url = "",
                      Icon = "fa fa-external-link-square",
                      MenuId = 5,
                     },
                            new Pagina {
                        Id = 32,
                      Codigo = 26,
                      Nome = "Restaurar dados sistema",
                      Url = "",
                      Icon = "fa fa-refresh",
                      MenuId = 6,
                     }


                };
                _context.Pagina.AddRange(novaPagina);



                await _context.SaveChangesAsync();
              

                return Ok("Menu restaurado");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar paginas. Erro: {ex.Message}");
            }
        }

        
       
    }
}
