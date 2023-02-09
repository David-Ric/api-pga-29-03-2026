using Microsoft.EntityFrameworkCore;

namespace PortalGrupoAlyne.Persist
{
    public interface IMenuPersist
    {
        Task<Menu> GetMenuIdAsync(int id);
    }
    public class MenuPersist : IMenuPersist
    {
        private readonly DataContext _context;
        public MenuPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<Menu> GetMenuIdAsync(int id)
        {
            IQueryable<Menu> query = _context.Menu.Include("SubMenu").Include("SubMenu.Pagina").Include("Pagina");

            query = query.AsNoTracking().OrderBy(e => e.Ordem).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        
    }
}
