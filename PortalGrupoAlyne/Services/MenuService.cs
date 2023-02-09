using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IMenuService
    {
        void Update(int id, MenuDto model);
        Task<Menu> GetMenuIdAsync(int id);
    }
    public class MenuService : IMenuService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMenuPersist _menuPresist;
        public MenuService(
                IMenuPersist menuPersist,
                DataContext context,
           IMapper mapper)
        {
            _menuPresist = menuPersist;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, MenuDto model)
        {
            var menu = getMenu(id);

            if (menu.Id != model.Id && _context.Menu.Any(x => x.Id == model.Id))
                throw new AppException("Menu não encontrado!");


            // copy model to user and save
            _mapper.Map(model, menu);
            _context.Menu.Update(menu);
            _context.SaveChanges(); ;
        }

        private Menu getMenu(int id)
        {
            var menu = _context.Menu.Find(id);
            if (menu == null) throw new KeyNotFoundException("Menu não encontrado!");
            return menu;
        }
        public async Task<Menu> GetMenuIdAsync(int id)
        {
            try
            {
                var menu = await _menuPresist.GetMenuIdAsync(id);
                if (menu== null) return null;

                var resultado = _mapper.Map<Menu>(menu);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
