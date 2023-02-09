using AutoMapper;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;

namespace PortalGrupoAlyne.Services
{
    public interface ITipoNegociacaoService
    {
        void Update(int id, TipoNegociacaoDto model);
    }
    public class TipoNegociacaoService : ITipoNegociacaoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public TipoNegociacaoService(
             DataContext context,
           IMapper mapper
            ) 
        {
            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, TipoNegociacaoDto model)
        {
            var negociacao = getTipoNegociacao(id);

            // validate
            if (model.Id != negociacao.Id && _context.TipoNegociacao.Any(x => x.Id == model.Id))
                throw new AppException("Tipo Negociação não encontrada!");


            // copy model to user and save
            _mapper.Map(model, negociacao);
            _context.TipoNegociacao.Update(negociacao);
            _context.SaveChanges(); ;
        }
        private TipoNegociacao getTipoNegociacao(int id)
        {
            var negociacao = _context.TipoNegociacao.Find(id);
            if (negociacao == null) throw new KeyNotFoundException("Tipo Negociação não encontrada!");
            return negociacao;
        }
    }
}
