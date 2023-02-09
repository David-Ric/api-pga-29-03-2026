namespace PortalGrupoAlyne.Persist
{
    public interface IItemTabelaPersist
    {
        Task<ItemTabela> GetItemTabelaAsync(int id);
    }
    public class ItemTabelaPersist : IItemTabelaPersist
    {
        private readonly DataContext _context;
        public ItemTabelaPersist(DataContext context)
        {
            _context = context;
        }
        public async Task<ItemTabela> GetItemTabelaAsync(int Id)
        {
            IQueryable<ItemTabela> query = _context.ItemTabela
                .Include(e => e.Produtos);

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
