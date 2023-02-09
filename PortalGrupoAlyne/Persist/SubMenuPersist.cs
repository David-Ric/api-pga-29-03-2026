namespace PortalGrupoAlyne.Persist
{
    public interface ISubMenuPersist
    {
        Task<SubMenu> GetMenuIdAsync(int id);
    }
    public class SubMenuPersist : ISubMenuPersist
    {
        private readonly DataContext _context;
        public SubMenuPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<SubMenu> GetMenuIdAsync(int id)
        {
            IQueryable<SubMenu> query = _context.SubMenu.Include("Pagina");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}
