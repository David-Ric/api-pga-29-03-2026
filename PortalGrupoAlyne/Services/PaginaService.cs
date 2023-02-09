using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;
using PortalGrupoAlyne.Persist.Contratos;

namespace PortalGrupoAlyne.Services
{
    public interface IPaginaService
    {
        void Update(int id, PaginaDto model);
        Task<Pagina[]> GetPaginasPorMenuId(int menuId);
    }
    public class PaginaService : IPaginaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPaginaPersist _paginaPersist;
       

        public PaginaService(
            DataContext context,
            IPaginaPersist paginaPerisit,
            IMapper mapper)
        {
            _paginaPersist = paginaPerisit;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, PaginaDto model)
        {
            var pagina = getPagina(id);

            // validate
            if (model.Id != pagina.Id && _context.Pagina.Any(x => x.Id == model.Id))
                throw new AppException("Página não encontrada");


            // copy model to user and save
            _mapper.Map(model, pagina);
            _context.Pagina.Update(pagina);
            _context.SaveChanges();
        }

        private Pagina getPagina(int id)
        {
            var pagina = _context.Pagina.Find(id);
            if (pagina == null) throw new KeyNotFoundException("Página não encontrada!");
            return pagina;
        }

        public async Task<Pagina[]> GetPaginasPorMenuId(int menuId)
        {
            try
            {
                var paginas = await _paginaPersist.GetPaginasPorMenuId(menuId);
                if (paginas == null) return null;

                var resultado = _mapper.Map<Pagina[]>(paginas);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
