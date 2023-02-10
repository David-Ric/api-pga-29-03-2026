namespace PortalGrupoAlyne.Persist
{
    public interface IProdutoPersist
    {
        Task<Produto> GetProdutoId(int id);
    }
    public class ProdutoPersist : IProdutoPersist
    {
        private readonly DataContext _context;
        public ProdutoPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetProdutoId(int id)
        {
            IQueryable<Produto> query = _context.Produto.Include("GrupoProduto");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
