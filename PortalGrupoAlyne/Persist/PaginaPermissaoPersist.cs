using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Persist
{
        public interface IPaginaPermissaoPersist
        {
            Task<PaginaPermissao[]> GetPaginasPorMenuId(int menuId);
        }
        public class PaginaPermissaoPersist : IPaginaPermissaoPersist
        {
            private readonly DataContext _context;
            public PaginaPermissaoPersist(DataContext context)
            {
                _context = context;
            }

            public async Task<PaginaPermissao[]> GetPaginasPorMenuId(int menuId)
            {
                IQueryable<PaginaPermissao> query = _context.PaginaPermissao;
                query = query.AsNoTracking()
                             .Where(p => p.MenuPermissaoId == menuId);

                return await query.ToArrayAsync();
            }
        }

    }
