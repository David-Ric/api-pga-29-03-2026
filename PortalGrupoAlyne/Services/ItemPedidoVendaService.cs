using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IItemPedidoVendaService
    {
        void Update(int id, ItemPedidoVendaDto model);
        Task<ItemPedidoVenda> GetItemPedidoVendaByIdAsync(int id);
    }
    public class ItemPedidoVendaService : IItemPedidoVendaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IItemPedidoVendaPersist _itemPedidoVendaPresist;
        public ItemPedidoVendaService(
                IItemPedidoVendaPersist itemPedidoVendaPersist,
                DataContext context,
           IMapper mapper)
        {
            _itemPedidoVendaPresist = itemPedidoVendaPersist;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemPedidoVenda> GetItemPedidoVendaByIdAsync(int id)
        {
            try
            {
                var item = await _itemPedidoVendaPresist.GetItemPedidoVendaByIdAsync(id);
                if (item == null) return null;

                var resultado = _mapper.Map<ItemPedidoVenda>(item);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, ItemPedidoVendaDto model)
        {
            var item = getItemPedidoVenda(id);

            if (item.Id != model.Id && _context.ItemPedidoVenda.Any(x => x.Id == model.Id))
                throw new AppException("Item não encontrada!");


            // copy model to user and save
            _mapper.Map(model, item);
            _context.ItemPedidoVenda.Update(item);
            _context.SaveChanges(); ;
        }
        private ItemPedidoVenda getItemPedidoVenda(int id)
        {
            var item = _context.ItemPedidoVenda.Find(id);
            if (item == null) throw new KeyNotFoundException("Item não encontrada!");
            return item;
        }
    }
}
