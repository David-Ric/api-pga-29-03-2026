using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ICabecalhoPedidoVendaService
    {
        void Update(int id, CabecalhoPedidoVendaDto model);
        Task<CabecalhoPedidoVenda> GetPedidoVendaByIdAsync(int id);
    }
    public class CabecalhoPedidoVendaService : ICabecalhoPedidoVendaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICabecalhoPedidoVendaPersist _cabecalhoPedidoVendaPresist;
        public CabecalhoPedidoVendaService(
                ICabecalhoPedidoVendaPersist cabecalhoPedidoVendaPersist,
                DataContext context,
           IMapper mapper)
        {
            _cabecalhoPedidoVendaPresist = cabecalhoPedidoVendaPersist;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabecalhoPedidoVenda> GetPedidoVendaByIdAsync(int id)
        {
            try
            {
                var cabecalho = await _cabecalhoPedidoVendaPresist.GetCabecalhoByIdAsync(id);
                if (cabecalho == null) return null;

                var resultado = _mapper.Map<CabecalhoPedidoVenda>(cabecalho);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, CabecalhoPedidoVendaDto model)
        {
            var cabecalho = getCabecalhoPedidoVenda(id);

            if (cabecalho.Id != model.Id && _context.CabecalhoPedidoVenda.Any(x => x.Id == model.Id))
                throw new AppException("Pedido de Venda não encontrada!");


            // copy model to user and save
            _mapper.Map(model, cabecalho);
            _context.CabecalhoPedidoVenda.Update(cabecalho);
            _context.SaveChanges(); ;
        }
        private CabecalhoPedidoVenda getCabecalhoPedidoVenda(int id)
        {
            var cabecalho = _context.CabecalhoPedidoVenda.Find(id);
            if (cabecalho == null) throw new KeyNotFoundException("Pedido de Venda não encontrada!");
            return cabecalho;
        }
    }
}
