using Microsoft.Extensions.Logging;
using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Persist
{
    public interface ITabelaPrecoPersist
    {
        Task<TabelaPreco> GetTabelaByIdAsync(int Id);
    }
    public class TabelaPrecoPersist : ITabelaPrecoPersist
    {
        private readonly DataContext _context;
        public TabelaPrecoPersist(DataContext context)
        {
            _context= context;
        }
        public async Task<TabelaPreco> GetTabelaByIdAsync(int Id)
        {
            IQueryable<TabelaPreco> query = _context.TabelaPreco
                .Include("ItemTabela").Include("ItemTabela.Produtos");
              
            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e=> e.Id == Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
