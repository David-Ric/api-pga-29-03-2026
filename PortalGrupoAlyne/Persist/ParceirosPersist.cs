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

        //public async Task<Parceiro> GetParceirosId(int id)
        //{
        //    IQueryable<Parceiro> query = _context.Parceiro
        //        .Include("Vendedor").Include("TabelaPrecoParceiro").Include("TabelaPrecoParceiro.Empresa").Include("TabelaPrecoParceiro.TabelaPreco").Include("Titulo"); 

        //    query = query.AsNoTracking().OrderBy(e => e.id).Where(e => e.id == id);

        //    return await query.FirstOrDefaultAsync();
        //}

     

        public async Task<Parceiro> GetParceirosId(int id)
        {
            IQueryable<Parceiro> query = _context.Parceiro
                .Include("Vendedor").Include("TabelaPrecoParceiro").Include("TabelaPrecoParceiro.Empresa").Include("TabelaPrecoParceiro.TabelaPreco").Include("Titulo");

            query = query.AsNoTracking().OrderBy(e => e.id).Where(e => e.id == id);

            var parceiro = await query.FirstOrDefaultAsync();

            if (parceiro != null)
            {
                var tipoNegociacaoId = int.Parse(parceiro.TipoNegociacao);

                var tipoNegociacao = await _context.TipoNegociacao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tn => tn.Id == tipoNegociacaoId);

                if (tipoNegociacao != null)
                {
                    parceiro.DescTipoNegociacao = tipoNegociacao.Descricao;
                }
            }

            return parceiro;
            }


    }
}
