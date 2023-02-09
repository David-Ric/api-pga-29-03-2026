using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;
using static PortalGrupoAlyne.Persist.PaginaPermissaoPersist;

namespace PortalGrupoAlyne.Services
{
    public interface IPaginaPermissaoService
    {
        void Update(int id, PaginaPermissaoDto model);
        Task<PaginaPermissao[]> GetPaginasPorMenuId(int menuId);
    }
    public class PaginaPermissaoService : IPaginaPermissaoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPaginaPermissaoPersist _paginaPermissaoPersist;


        public PaginaPermissaoService(
            DataContext context,
            IPaginaPermissaoPersist paginaPermissaoPerisit,
            IMapper mapper)
        {
            _paginaPermissaoPersist = paginaPermissaoPerisit;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, PaginaPermissaoDto model)
        {
            var pagina = getPagina(id);

            // validate
            if (model.Id != pagina.Id && _context.PaginaPermissao.Any(x => x.Id == model.Id))
                throw new AppException("Página não encontrada");


            // copy model to user and save
            _mapper.Map(model, pagina);
            _context.PaginaPermissao.Update(pagina);
            _context.SaveChanges();
        }

        private PaginaPermissao getPagina(int id)
        {
            var pagina = _context.PaginaPermissao.Find(id);
            if (pagina == null) throw new KeyNotFoundException("Página não encontrada!");
            return pagina;
        }

        public async Task<PaginaPermissao[]> GetPaginasPorMenuId(int menuId)
        {
            try
            {
                var paginas = await _paginaPermissaoPersist.GetPaginasPorMenuId(menuId);
                if (paginas == null) return null;

                var resultado = _mapper.Map<PaginaPermissao[]>(paginas);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
