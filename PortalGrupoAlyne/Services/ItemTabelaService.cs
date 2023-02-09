using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IItemTabelaService
    {
        void Update(int id, ItemTabelaDto model);
        Task<ItemTabelaDto> GetItemTabelaAsync(int id);
    }
    public class ItemTabelaService : IItemTabelaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IItemTabelaPersist _itemTabelaPersist;

        public ItemTabelaService(DataContext context, IItemTabelaPersist itemTabelaPersist,
           IMapper mapper
        )
        {
            _itemTabelaPersist = itemTabelaPersist;
            _context = context;
            _mapper = mapper;
        }



        public void Update(int id, ItemTabelaDto model)
        {
            var item = getItemTabela(id);

            if (item.Id != model.Id && _context.ItemTabela.Any(x => x.Id == model.Id))
                throw new AppException("Item da Tabela de Preço não encontrado!");


            // copy model to user and save
            _mapper.Map(model, item);
            _context.ItemTabela.Update(item);
            _context.SaveChanges(); ;
        }
        private ItemTabela getItemTabela(int id)
        {
            var item = _context.ItemTabela.Find(id);
            if (item == null) throw new KeyNotFoundException("Item da Tabela de Preço não encontrado!");
            return item;
        }

        public async Task<ItemTabelaDto> GetItemTabelaAsync(int id)
        {
            try
            {
                var item = await _itemTabelaPersist.GetItemTabelaAsync(id);
                if (item == null) return null;

                var resultado = _mapper.Map<ItemTabelaDto>(item);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
