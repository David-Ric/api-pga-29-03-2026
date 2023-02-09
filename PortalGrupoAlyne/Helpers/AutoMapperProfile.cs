using AutoMapper;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Model.Dtos.Usuarios;

namespace PortalGrupoAlyne.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            // CreateMap<CreateRequest, User>();

            // UpdateRequest -> User
            CreateMap<UserUpdateResquest, Usuario>();
            CreateMap<Vendedor, VendedorDto>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Concorrente, ConcorrenteDto>().ReverseMap();
            CreateMap<ProdutoConcorrente, ProdutoConcorrenteDto>().ReverseMap();
            CreateMap<Parceiro, ParceiroDto>().ReverseMap();
            CreateMap<TipoNegociacao, TipoNegociacaoDto>().ReverseMap();
            CreateMap<TabelaPreco, TabelaPrecoDto>().ReverseMap();
            CreateMap<ItemTabela, ItemTabelaDto>().ReverseMap();
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<TabelaPrecoCliente, TabelaPrecoClienteDto>().ReverseMap();
            CreateMap<Usuario, UserDto>().ReverseMap();
            CreateMap<PaginaBase, PaginaBaseDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Pagina, PaginaDto>().ReverseMap();
            CreateMap<SubMenu, SubMenuDto>().ReverseMap();
            CreateMap<GrupoUsuario, GrupoUsuarioDto>().ReverseMap();
            CreateMap<MenuPermissao,MenuPermissaoDto>().ReverseMap();
            CreateMap<SubMenuPermissao,SubMenuPermissaoDto>().ReverseMap();
            CreateMap<PaginaPermissao, PaginaPermissaoDto>().ReverseMap();

        }
    }
}