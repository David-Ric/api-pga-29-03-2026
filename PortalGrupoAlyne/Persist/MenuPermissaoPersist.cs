namespace PortalGrupoAlyne.Persist
{
    public interface IMenuPermissoesPersist
    {
        Task<MenuPermissao> GetMenuIdAsync(int id);
    }
    public class MenuPermissaoPersist : IMenuPermissoesPersist
    {
        private readonly DataContext _context;
        public MenuPermissaoPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<MenuPermissao> GetMenuIdAsync(int id)
        {
            IQueryable<MenuPermissao> query = _context.MenuPermissao.Include("SubMenuPermissao").Include("SubMenuPermissao.PaginaPermissao").Include("PaginaPermissao");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}
