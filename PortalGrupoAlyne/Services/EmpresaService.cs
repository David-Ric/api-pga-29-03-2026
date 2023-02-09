using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Services
{
    public interface IEmpresaService
    {
        void Update(int id, EmpresaDto model);
    }
    public class EmpresaService : IEmpresaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public EmpresaService(
           DataContext context,
           IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public void Update(int id, EmpresaDto model)
        {

            var empresa = getEmpresa(id);

            // validate
            if (model.Id != empresa.Id && _context.Empresa.Any(x => x.Id == model.Id))
                throw new AppException("Empresa não encontrado");


            // copy model to user and save
            _mapper.Map(model, empresa);
            _context.Empresa.Update(empresa);
            _context.SaveChanges(); ;
        }
        private Empresa getEmpresa(int id)
        {
            var empresa = _context.Empresa.Find(id);
            if (empresa == null) throw new KeyNotFoundException("Empresa não encontrada!");
            return empresa;
        }
    }
}
