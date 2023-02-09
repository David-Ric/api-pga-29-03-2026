using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Persist.Contratos;

namespace PortalGrupoAlyne.Persist
{
    public class VendedorPersist : IVendedorPersist
    {
        private readonly DataContext _context;

        public VendedorPersist(DataContext context) 
        {
            _context = context;
        }
        public async Task<Vendedor> GetVendedoreTipoAsync(string tipo)
        {
            return await _context.Vendedor
                                  .SingleOrDefaultAsync(vendedor => vendedor.Tipo == tipo.ToLower());
        }
    }
}
