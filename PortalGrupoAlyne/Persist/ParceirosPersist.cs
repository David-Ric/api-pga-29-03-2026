namespace PortalGrupoAlyne.Persist
{
    public interface IParceirosPersist
    {
        Task<Parceiro> GetParceirosId(int id);
    }
    public class ParceirosPersist : IParceirosPersist
    {
        private readonly DataContext _context;
        public ParceirosPersist(DataContext context)
        {
            _context = context;
        }

        public async Task<Parceiro> GetParceirosId(int id)
        {
            IQueryable<Parceiro> query = _context.Parceiro
                .Include("Vendedor");

            query = query.AsNoTracking().OrderBy(e => e.id).Where(e => e.id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
