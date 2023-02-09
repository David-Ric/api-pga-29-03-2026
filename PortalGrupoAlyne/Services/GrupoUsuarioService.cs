using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IUserGrupoService
    {
        void Update(int id, GrupoUsuarioDto model);
        Task<GrupoUsuario> GetUserGrupoByIdAsync(int id);
    }
    public class GrupoUsuarioService : IUserGrupoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserGrupoPersist _userGrupoPresist;
        public GrupoUsuarioService(
                IUserGrupoPersist userGrupoPersist,
                DataContext context,
           IMapper mapper)
        {
            _userGrupoPresist = userGrupoPersist;
            _context = context;
            _mapper = mapper;
        }

        public async Task<GrupoUsuario> GetUserGrupoByIdAsync(int id)
        {
            try
            {
                var grupo = await _userGrupoPresist.GetGrupoIdAsync(id);
                if (grupo == null) return null;

                var resultado = _mapper.Map<GrupoUsuario>(grupo);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, GrupoUsuarioDto model)
        {
            var grupo = getTabelaPreco(id);

            if (grupo.Id != model.Id && _context.GrupoUsuario.Any(x => x.Id == model.Id))
                throw new AppException("Tabela de Preço não encontrada!");


            // copy model to user and save
            _mapper.Map(model, grupo);
            _context.GrupoUsuario.Update(grupo);
            _context.SaveChanges(); ;
        }
        private GrupoUsuario getTabelaPreco(int id)
        {
            var grupo = _context.GrupoUsuario.Find(id);
            if (grupo == null) throw new KeyNotFoundException("Tabela de Preço não encontrada!");
            return grupo;
        }
    }
}
