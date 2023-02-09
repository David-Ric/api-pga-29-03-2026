using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Services
{
    public interface IConcorrentesService
    {
        void Update(int id, ConcorrenteDto model);
    }
    public class ConcorrenteService : IConcorrentesService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public ConcorrenteService(
           DataContext context,
           IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, ConcorrenteDto model)
        {
            var concorrente = getConcorrente(id);

            // validate
            if (model.Id != concorrente.Id && _context.Concorrente.Any(x => x.Id == model.Id))
                throw new AppException("Produto não encontrado");


            // copy model to user and save
            _mapper.Map(model, concorrente);
            _context.Concorrente.Update(concorrente);
            _context.SaveChanges(); ;
        }
        private Concorrente getConcorrente(int id)
        {
            var concorrente = _context.Concorrente.Find(id);
            if (concorrente == null) throw new KeyNotFoundException("Concorrente não encontrado!");
            return concorrente;
        }
    }
}
