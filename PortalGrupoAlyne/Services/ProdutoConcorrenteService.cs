using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Services
{
    public interface IProdutoConcorrenteService
    {
        void Update(int id, ProdutoConcorrenteDto model);
    }
    public class ProdutoConcorrenteService : IProdutoConcorrenteService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public ProdutoConcorrenteService(
           DataContext context,
           IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, ProdutoConcorrenteDto model)
        {
            var prodconcorrente = getProdutoConcorrente(id);

            // validate
            if (model.Id != prodconcorrente.Id && _context.ProdutoConcorrente.Any(x => x.Id == model.Id))
                throw new AppException("Produto concorrente não encontrado");


            // copy model to user and save
            _mapper.Map(model, prodconcorrente);
            _context.ProdutoConcorrente.Update(prodconcorrente);
            _context.SaveChanges(); ;
        }

        private ProdutoConcorrente getProdutoConcorrente(int id)
        {
            var prodconcorrente = _context.ProdutoConcorrente.Find(id);
            if (prodconcorrente == null) throw new KeyNotFoundException("Produto concorrente não encontrado!");
            return prodconcorrente;
        }
    }
}
