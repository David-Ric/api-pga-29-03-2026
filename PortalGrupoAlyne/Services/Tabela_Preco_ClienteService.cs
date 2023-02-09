using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ITabela_Preco_ClienteService 
    {
        void Update(int id, TabelaPrecoClienteDto model);
        Task<TabelaPrecoCliente> GetTabelaClienteAsync(int id);
    }
    public class Tabela_Preco_ClienteService : ITabela_Preco_ClienteService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITabela_Preco_ClientePersist _tabela_Preco_ClientePersist;
        public Tabela_Preco_ClienteService(
           DataContext context,
           ITabela_Preco_ClientePersist tabela_Preco_ClientePersist,
           IMapper mapper)
        {
            _tabela_Preco_ClientePersist = tabela_Preco_ClientePersist;
            _context = context;
            _mapper = mapper;
        }

        public void Update(int id, TabelaPrecoClienteDto model)
        {
            var tabela = getTabela(id);

            // validate
            if (model.id != tabela.id && _context.TabelaPrecoCliente.Any(x => x.id == model.id))
                throw new AppException("Tabela de preço não encontrada");


            // copy model to user and save
            _mapper.Map(model, tabela);
            _context.TabelaPrecoCliente.Update(tabela);
            _context.SaveChanges(); ;
        }

        private TabelaPrecoCliente getTabela(int id)
        {
            var tabela = _context.TabelaPrecoCliente.Find(id);
            if (tabela == null) throw new KeyNotFoundException("Tabela de preço não encontrada!");
            return tabela;
        }

        public async Task<TabelaPrecoCliente> GetTabelaClienteAsync(int id)
        {
            try
            {
                var tabela = await _tabela_Preco_ClientePersist.GetTabelaClienteAsync(id);
                if (tabela == null) return null;

                var resultado = _mapper.Map<TabelaPrecoCliente>(tabela);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
