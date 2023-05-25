using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ITabelaPrecoAdicionalService
    {
        void Update(int id, TabelaPrecoAdicionalDto model);
        Task<TabelaPrecoAdicional> GetTabelaByIdAsync(int id);
    }
    public class TabelaPrecoAdicionalService : ITabelaPrecoAdicionalService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITabelaPrecoAdicionalPersist _tabelaPrecoAdicionalPresist;
        public TabelaPrecoAdicionalService(
                ITabelaPrecoAdicionalPersist tabelaPrecoAdicionalPersist,
                DataContext context,
           IMapper mapper)
        {
            _tabelaPrecoAdicionalPresist = tabelaPrecoAdicionalPersist;
            _context = context;
            _mapper = mapper;
        }

        public async Task<TabelaPrecoAdicional> GetTabelaByIdAsync(int id)
        {
            try
            {
                var tabela = await _tabelaPrecoAdicionalPresist.GetTabelaByIdAsync(id);
                if (tabela == null) return null;

                var resultado = _mapper.Map<TabelaPrecoAdicional>(tabela);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, TabelaPrecoAdicionalDto model)
        {
            var tabela = getTabelaPreco(id);

            if (tabela.Id != model.Id && _context.TabelaPrecoAdicional.Any(x => x.Id == model.Id))
                throw new AppException("Tabela de Preço não encontrada!");


            // copy model to user and save
            _mapper.Map(model, tabela);
            _context.TabelaPrecoAdicional.Update(tabela);
            _context.SaveChanges(); ;
        }
        private TabelaPrecoAdicional getTabelaPreco(int id)
        {
            var tabela = _context.TabelaPrecoAdicional.Find(id);
            if (tabela == null) throw new KeyNotFoundException("Tabela de Preço não encontrada!");
            return tabela;
        }
    }
}

