namespace PortalGrupoAlyne.Persist
{
    public interface ITabela_Preco_ClientePersist
    {
        Task<TabelaPrecoCliente> GetTabelaClienteAsync(int id);
    }
    public class Tabela_Preco_ClientePersist:ITabela_Preco_ClientePersist
    {
        private readonly DataContext _context;
        public Tabela_Preco_ClientePersist(DataContext context)
        {
            _context = context;
        }

        public async Task<TabelaPrecoCliente> GetTabelaClienteAsync(int id)
        {
            IQueryable<TabelaPrecoCliente> query = _context.TabelaPrecoCliente
               .Include(e => e.TabelaPreco);

            query = query.AsNoTracking().OrderBy(e => e.id).Where(e => e.id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
