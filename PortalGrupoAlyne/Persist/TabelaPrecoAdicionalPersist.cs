using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Persist
{
    public interface ITabelaPrecoAdicionalPersist
    {
        Task<TabelaPrecoAdicional> GetTabelaByIdAsync(int Id);
    }
    public class TabelaPrecoAdiconalPersist : ITabelaPrecoAdicionalPersist
    {
        private readonly DataContext _context;
        public TabelaPrecoAdiconalPersist(DataContext context)
        {
            _context = context;
        }
        public async Task<TabelaPrecoAdicional> GetTabelaByIdAsync(int Id)
        {
            IQueryable<TabelaPrecoAdicional> query = _context.TabelaPrecoAdicional
                .Include("ItemTabela").Include("ItemTabela.Produtos");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
