using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ITabelaPrecoParceiroService
    {
        void Update(int id, TabelaPrecoParceiroDto model);
        Task<TabelaPrecoParceiro> GetTabelaClienteAsync(int id);
    }
    public class TabelaPrecoParceiroService : ITabelaPrecoParceiroService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITabelaPrecoParceiroPersist _tabelaPrecoParceiroPersist;
        public TabelaPrecoParceiroService(
           DataContext context,
           ITabelaPrecoParceiroPersist tabelaPrecoParceiroPersist,
           IMapper mapper)
        {
            _tabelaPrecoParceiroPersist = tabelaPrecoParceiroPersist;
            _context = context;
            _mapper = mapper;
        }

        public void Update(int id, TabelaPrecoParceiroDto model)
        {
            var tabela = getTabela(id);

            // validate
            if (model.id != tabela.id && _context.TabelaPrecoParceiro.Any(x => x.id == model.id))
                throw new AppException("Tabela de preço não encontrada");


            // copy model to user and save
            _mapper.Map(model, tabela);
            _context.TabelaPrecoParceiro.Update(tabela);
            _context.SaveChanges(); ;
        }

        private TabelaPrecoParceiro getTabela(int id)
        {
            var tabela = _context.TabelaPrecoParceiro.Find(id);
            if (tabela == null) throw new KeyNotFoundException("Tabela de preço não encontrada!");
            return tabela;
        }

        public async Task<TabelaPrecoParceiro> GetTabelaClienteAsync(int id)
        {
            try
            {
                var tabela = await _tabelaPrecoParceiroPersist.GetTabelaClienteAsync(id);
                if (tabela == null) return null;

                var resultado = _mapper.Map<TabelaPrecoParceiro>(tabela);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
