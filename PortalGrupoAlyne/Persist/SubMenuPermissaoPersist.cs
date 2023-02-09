namespace PortalGrupoAlyne.Persist
{
    public interface ISubMenuPermissaoPersist
    {
        Task<SubMenuPermissao> GetMenuIdAsync(int id);
    }
    public class SubMenuPermissaoPersist : ISubMenuPermissaoPersist
    {
        private readonly DataContext _context;
        public SubMenuPermissaoPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<SubMenuPermissao> GetMenuIdAsync(int id)
        {
            IQueryable<SubMenuPermissao> query = _context.SubMenuPermissao.Include("PaginaPermissao");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}
