namespace PortalGrupoAlyne.Persist
{
    public interface IUserGrupoPersist
    {
        Task<GrupoUsuario> GetGrupoIdAsync(int id);
    }
    public class GrupoUsuarioPersist : IUserGrupoPersist
    {
        private readonly DataContext _context;
        public GrupoUsuarioPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<GrupoUsuario> GetGrupoIdAsync(int id)
        {
            IQueryable<GrupoUsuario> query = _context.GrupoUsuario.Include("MenuPermissao").Include("MenuPermissao.SubMenuPermissao").Include("MenuPermissao.PaginaPermissao").Include("MenuPermissao.SubMenuPermissao.PaginaPermissao").Include("SubMenuPermissao.PaginaPermissao").Include("PaginaPermissao");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
