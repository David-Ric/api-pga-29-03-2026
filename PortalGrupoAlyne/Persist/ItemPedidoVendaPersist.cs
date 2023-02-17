using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Persist
{
    public interface IItemPedidoVendaPersist
    {
        Task<ItemPedidoVenda> GetItemPedidoVendaByIdAsync(int Id);
    }
    public class ItemPedidoVendaPersist : IItemPedidoVendaPersist
    {
        private readonly DataContext _context;
        public ItemPedidoVendaPersist(DataContext context)
        {
            _context = context;
        }
        public async Task<ItemPedidoVenda> GetItemPedidoVendaByIdAsync(int Id)
        {
            IQueryable<ItemPedidoVenda> query = _context.ItemPedidoVenda.Include("Produto");

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == Id);

            return await query.FirstOrDefaultAsync();
        }

    }
}
