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
            CreateMap<Usuario, UserDto>().ReverseMap();
            CreateMap<PaginaBase, PaginaBaseDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Pagina, PaginaDto>().ReverseMap();
            CreateMap<SubMenu, SubMenuDto>().ReverseMap();
            CreateMap<GrupoUsuario, GrupoUsuarioDto>().ReverseMap();
            CreateMap<MenuPermissao,MenuPermissaoDto>().ReverseMap();
            CreateMap<SubMenuPermissao,SubMenuPermissaoDto>().ReverseMap();
            CreateMap<PaginaPermissao, PaginaPermissaoDto>().ReverseMap();
            CreateMap<TabelaPrecoParceiro, TabelaPrecoParceiroDto>().ReverseMap();
            CreateMap<CabecalhoPedidoVenda, CabecalhoPedidoVendaDto>().ReverseMap();
            CreateMap<ItemPedidoVenda, ItemPedidoVendaDto>().ReverseMap();
            CreateMap<Configuracao, ConfiguracaoDto>().ReverseMap();
            CreateMap<Comunicado, ComunicadoDto>().ReverseMap();
            CreateMap<PostLido, PostLidoDto>().ReverseMap();
            CreateMap<PreferenciasUsuario, PreferenciasUsuarioDto>().ReverseMap();
            CreateMap<PermissaoRH, PermissaoRhDto>().ReverseMap();
            CreateMap<ComunicadoComercial, ComunicadoComercialDto>().ReverseMap();
            CreateMap<ComunicadoLido, ComunicadoLidoDto>().ReverseMap();
            CreateMap<Logs, LogsDto>().ReverseMap();
            CreateMap<Modulo,  ModuloDto>().ReverseMap();
            CreateMap<ColunaModulo, ColunaModuloDto>().ReverseMap();
            CreateMap<OpcaoCampo,OpcaoCampoDto>().ReverseMap();
            CreateMap<LigacaoTabela, LigacaoTabelaDto>().ReverseMap();

        }
    }
}