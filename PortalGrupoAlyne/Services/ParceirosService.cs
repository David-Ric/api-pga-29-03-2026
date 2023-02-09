using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IParceirosService
    {
        void Update(int id, ParceiroDto model);
        Task<Parceiro> GetParceirosId(int id);
    }
    public class ParceirosService : IParceirosService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IParceirosPersist _parceirosPersist;
        public ParceirosService(DataContext context,IParceirosPersist parceirosPersist,
           IMapper mapper) 
        {
            _parceirosPersist= parceirosPersist;
            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, ParceiroDto model)
        {
            var parceiro = getParceiro(id);

            // validate
            if (model.id != parceiro.id && _context.Parceiro.Any(x => x.id == model.id))
                throw new AppException("Parceiro não encontrado");


            // copy model to user and save
            _mapper.Map(model, parceiro);
            _context.Parceiro.Update(parceiro);
            _context.SaveChanges(); ;
        }
        private Parceiro getParceiro(int id)
        {
            var parceiro = _context.Parceiro.Find(id);
            if (parceiro == null) throw new KeyNotFoundException("Parceiro não encontrado!");
            return parceiro;
        }

        public async Task<Parceiro> GetParceirosId(int id)
        {
            try
            {
                var vendedor = await _parceirosPersist.GetParceirosId(id);
                if (vendedor == null) return null;

                var resultado = _mapper.Map<Parceiro>(vendedor);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
