using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne.Services
{
    public interface IProdutoService
    {
   
        void Update(int id, ProdutoDto model);
        Task<Produto> GetProdutoId(int id);

    }
    public class ProdutoService : IProdutoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IProdutoPersist _produtoPersist;
        public ProdutoService(
            IProdutoPersist produtoPersist,
           DataContext context,
           IMapper mapper)
        {
            _produtoPersist = produtoPersist;
            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, ProdutoDto model)
        {
            var produto = getProduto(id);

            // validate
            if (model.Id != produto.Id && _context.Produto.Any(x => x.Id == model.Id))
                throw new AppException("Produto não encontrado");


            // copy model to user and save
            _mapper.Map(model, produto);
            _context.Produto.Update(produto);
            _context.SaveChanges(); ;
        }
        private Produto getProduto(int id)
        {
            var produto = _context.Produto.Find(id);
            if (produto == null) throw new KeyNotFoundException("Produto não encontrado!");
            return produto;
        }
        public async Task<Produto> GetProdutoId(int id)
        {
            try
            {
                var produto = await _produtoPersist.GetProdutoId(id);
                if (produto == null) return null;

                var resultado = _mapper.Map<Produto>(produto);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
