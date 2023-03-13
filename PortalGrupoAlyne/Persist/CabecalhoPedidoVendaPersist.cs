namespace PortalGrupoAlyne.Persist
{
   
        public interface ICabecalhoPedidoVendaPersist
        {
            Task<CabecalhoPedidoVenda> GetCabecalhoByIdAsync(int Id);
        }
        public class CabecalhoPedidoVendaPersist : ICabecalhoPedidoVendaPersist
        {
            private readonly DataContext _context;
            public CabecalhoPedidoVendaPersist(DataContext context)
            {
                _context = context;
            }
            public async Task<CabecalhoPedidoVenda> GetCabecalhoByIdAsync(int Id)
            {
                IQueryable<CabecalhoPedidoVenda> query = _context.CabecalhoPedidoVenda
                    .Include("Vendedor").Include("Parceiro").Include("TipoNegociacao");

                query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == Id);

                return await query.FirstOrDefaultAsync();
            }
        
    }
}
