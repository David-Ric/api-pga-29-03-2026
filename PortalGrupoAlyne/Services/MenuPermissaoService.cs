using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IMenuPermissoesService
    {
        void Update(int id, MenuPermissaoDto model);
        Task<MenuPermissao> GetMenuIdAsync(int id);
    }
    public class MenuPermissaoService : IMenuPermissoesService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMenuPermissoesPersist _menuPermissoesPresist;
        public MenuPermissaoService(
                IMenuPermissoesPersist menuPermissoesPersist,
                DataContext context,
           IMapper mapper)
        {
            _menuPermissoesPresist = menuPermissoesPersist;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, MenuPermissaoDto model)
        {
            var menu = getMenu(id);

            if (menu.Id != model.Id && _context.MenuPermissao.Any(x => x.Id == model.Id))
                throw new AppException("Menu não encontrado!");


            // copy model to user and save
            _mapper.Map(model, menu);
            _context.MenuPermissao.Update(menu);
            _context.SaveChanges(); ;
        }

        private MenuPermissao getMenu(int id)
        {
            var menu = _context.MenuPermissao.Find(id);
            if (menu == null) throw new KeyNotFoundException("Menu não encontrado!");
            return menu;
        }
        public async Task<MenuPermissao> GetMenuIdAsync(int id)
        {
            try
            {
                var menu = await _menuPermissoesPresist.GetMenuIdAsync(id);
                if (menu == null) return null;

                var resultado = _mapper.Map<MenuPermissao>(menu);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
