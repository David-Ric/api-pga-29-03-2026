namespace PortalGrupoAlyne.Persist
{
    public interface ITabelaPrecoParceiroPersist
    {
        Task<TabelaPrecoParceiro> GetTabelaClienteAsync(int id);
    }
    public class TabelaPrecoParceiroPersist : ITabelaPrecoParceiroPersist
    {
        private readonly DataContext _context;
        public TabelaPrecoParceiroPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<TabelaPrecoParceiro> GetTabelaClienteAsync(int id)
        {
            IQueryable<TabelaPrecoParceiro> query = _context.TabelaPrecoParceiro
               .Include(e => e.TabelaPreco).Include(p => p.Empresa);

            query = query.AsNoTracking().OrderBy(e => e.EmpresaId).Where(e => e.id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
