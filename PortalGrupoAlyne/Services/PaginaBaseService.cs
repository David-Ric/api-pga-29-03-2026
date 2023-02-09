using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Services
{
    
        public interface IPaginaBaseService
        {
            void Update(int id, PaginaBaseDto model);
        }
        public class PaginaBaseService : IPaginaBaseService
        {
            private DataContext _context;
            private readonly IMapper _mapper;


            public PaginaBaseService(
                DataContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async void Update(int id, PaginaBaseDto model)
            {
                var pagina = getPagina(id);

                // validate
                if (model.Id != pagina.Id && _context.PaginaBase.Any(x => x.Id == model.Id))
                    throw new AppException("Página não encontrada");


                // copy model to user and save
                _mapper.Map(model, pagina);
                _context.PaginaBase.Update(pagina);
                _context.SaveChanges();
            }

            private PaginaBase getPagina(int id)
            {
                var pagina = _context.PaginaBase.Find(id);
                if (pagina == null) throw new KeyNotFoundException("Página não encontrada!");
                return pagina;
            }
        }
    }

