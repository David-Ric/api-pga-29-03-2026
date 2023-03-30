using PortalGrupoAlyne.Model.Dtos.Usuarios;

namespace PortalGrupoAlyne.Persist
{
    public interface IUsuarioPersist
    {
        Task<Usuario> GetUsuarioIdAsync(int id);
    }
    public class UsuarioPersist : IUsuarioPersist
    {
        private readonly DataContext _context;
        public UsuarioPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioIdAsync(int id)
        {
            IQueryable<Usuario> query = _context.Usuario
                .Include("MenuPermissao")
                .Include("MenuPermissao.SubMenuPermissao")
                .Include("MenuPermissao.PaginaPermissao")
                .Include("MenuPermissao.SubMenuPermissao.PaginaPermissao")
                .Include("SubMenuPermissao.PaginaPermissao")
                .Include("PaginaPermissao")
                .Include("GrupoUsuario")
                .Include("PostLido")
                .Include("ComunicadoLido")
                .Include("MensagensRecebidas")
                .Include("MensagensEnviadas");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
