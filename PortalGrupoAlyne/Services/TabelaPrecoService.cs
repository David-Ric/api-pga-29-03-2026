using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ITabelaPrecoService
    {
        void Update(int id, TabelaPrecoDto model);
        Task<TabelaPreco> GetTabelaByIdAsync(int id);
    }
    public class TabelaPrecoService : ITabelaPrecoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITabelaPrecoPersist _tabelaPrecoPresist;
        public TabelaPrecoService(
                ITabelaPrecoPersist tabelaPrecoPersist,
                DataContext context,
           IMapper mapper) 
        {
            _tabelaPrecoPresist = tabelaPrecoPersist;
            _context = context;
            _mapper = mapper;
        }

        public async Task<TabelaPreco> GetTabelaByIdAsync(int id)
        {
            try
            {
                var tabela = await _tabelaPrecoPresist.GetTabelaByIdAsync(id);
                if (tabela == null) return null;

                var resultado = _mapper.Map<TabelaPreco>(tabela);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, TabelaPrecoDto model)
        {
            var tabela = getTabelaPreco(id);

            if (tabela.Id != model.Id && _context.TabelaPreco.Any(x => x.Id == model.Id))
                throw new AppException("Tabela de Preço não encontrada!");


            // copy model to user and save
            _mapper.Map(model, tabela);
            _context.TabelaPreco.Update(tabela);
            _context.SaveChanges(); ;
        }
        private TabelaPreco getTabelaPreco(int id)
        {
            var tabela = _context.TabelaPreco.Find(id);
            if (tabela == null) throw new KeyNotFoundException("Tabela de Preço não encontrada!");
            return tabela;
        }
    }
}
