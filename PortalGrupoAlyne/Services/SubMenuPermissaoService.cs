using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface ISubMenuPermissaoService
    {
        void Update(int id, SubMenuPermissaoDto model);
        Task<SubMenuPermissao> GetMenuIdAsync(int id);
    }
    public class SubMenuPermissaoService : ISubMenuPermissaoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ISubMenuPermissaoPersist _subMenuPermissaoPresist;
        public SubMenuPermissaoService(
                ISubMenuPermissaoPersist subMenuPermissaoPersist,
                DataContext context,
           IMapper mapper)
        {
            _subMenuPermissaoPresist = subMenuPermissaoPersist;
            _context = context;
            _mapper = mapper;
        }

        public async void Update(int id, SubMenuPermissaoDto model)
        {
            var menu = getMenu(id);

            if (menu.Id != model.Id && _context.SubMenuPermissao.Any(x => x.Id == model.Id))
                throw new AppException("Menu não encontrado!");


            // copy model to user and save
            _mapper.Map(model, menu);
            _context.SubMenuPermissao.Update(menu);
            _context.SaveChanges(); ;
        }

        private SubMenuPermissao getMenu(int id)
        {
            var menu = _context.SubMenuPermissao.Find(id);
            if (menu == null) throw new KeyNotFoundException("Menu não encontrado!");
            return menu;
        }
        public async Task<SubMenuPermissao> GetMenuIdAsync(int id)
        {
            try
            {
                var menu = await _subMenuPermissaoPresist.GetMenuIdAsync(id);
                if (menu == null) return null;

                var resultado = _mapper.Map<SubMenuPermissao>(menu);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
