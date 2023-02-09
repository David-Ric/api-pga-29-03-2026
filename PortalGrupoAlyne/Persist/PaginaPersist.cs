using Microsoft.Extensions.Logging;

namespace PortalGrupoAlyne.Persist
{
    public interface IPaginaPersist
    {
        Task<Pagina[]> GetPaginasPorMenuId(int menuId);
    }
    public class PaginaPersist : IPaginaPersist
    {
        private readonly DataContext _context;
        public PaginaPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<Pagina[]> GetPaginasPorMenuId(int menuId)
        {
            IQueryable<Pagina> query = _context.Pagina;
            query = query.AsNoTracking()
                         .Where(p => p.MenuId == menuId);

            return await query.ToArrayAsync();
        }
    }
}
