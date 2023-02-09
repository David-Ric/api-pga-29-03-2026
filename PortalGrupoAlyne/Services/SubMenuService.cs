using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ISubMenuService
    {
        void Update(int id, SubMenuDto model);
        Task<SubMenu> GetMenuIdAsync(int id);
    }
    public class SubMenuService : ISubMenuService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ISubMenuPersist _subMenuPresist;
        public SubMenuService(
                ISubMenuPersist subMenuPersist,
                DataContext context,
           IMapper mapper)
        {
            _subMenuPresist = subMenuPersist;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, SubMenuDto model)
        {
            var menu = getMenu(id);

            if (menu.Id != model.Id && _context.SubMenu.Any(x => x.Id == model.Id))
                throw new AppException("Menu não encontrado!");


            // copy model to user and save
            _mapper.Map(model, menu);
            _context.SubMenu.Update(menu);
            _context.SaveChanges(); ;
        }

        private SubMenu getMenu(int id)
        {
            var menu = _context.SubMenu.Find(id);
            if (menu == null) throw new KeyNotFoundException("Menu não encontrado!");
            return menu;
        }
        public async Task<SubMenu> GetMenuIdAsync(int id)
        {
            try
            {
                var menu = await _subMenuPresist.GetMenuIdAsync(id);
                if (menu == null) return null;

                var resultado = _mapper.Map<SubMenu>(menu);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
