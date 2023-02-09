using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Model.Dtos;
using PortalGrupoAlyne.Persist.Contratos;

namespace PortalGrupoAlyne.Services
{
    public interface IVendedorService
    {
        void Update(int id, VendedorDto model);
        Task<Vendedor> GetVendedoreTipoAsync(string tipo);

    }

    public class VendedorService : IVendedorService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly IVendedorPersist _vendedorPersist;

        public VendedorService(
            IVendedorPersist vendedorPersist,
            DataContext context,
            IMapper mapper)
        {
            _vendedorPersist = vendedorPersist;
            _context = context;
            _mapper = mapper;
        }
        public void Update(int id, VendedorDto model)
        {
            var vendedor = getVendedor(id);

            // validate
            if (model.Id != vendedor.Id && _context.Vendedor.Any(x => x.Id == model.Id))
                throw new AppException("Vendedor não encontrado");


            // copy model to user and save
            _mapper.Map(model, vendedor);
            _context.Vendedor.Update(vendedor);
            _context.SaveChanges();
        }
        private Vendedor getVendedor(int id)
        {
            var vendedor = _context.Vendedor.Find(id);
            if (vendedor == null) throw new KeyNotFoundException("Vendedor não encontrado!");
            return vendedor;
        }

        public async Task<Vendedor> GetVendedoreTipoAsync(string tipo)
        {
            try
            {
                var vendedor = await _vendedorPersist.GetVendedoreTipoAsync(tipo);
                if (vendedor == null) return null;

                var vendedores = _mapper.Map<Vendedor>(vendedor);
                return vendedores;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Genrente não encontrado");
            }
        }
    }
}
