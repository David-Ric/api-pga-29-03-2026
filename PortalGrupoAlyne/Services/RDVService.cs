using PortalGrupoAlyne.Interfaces;
using PortalGrupoAlyne.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalGrupoAlyne.Services
{
    public class RDVService
    {
        private readonly IRDVRepository _repository;

        public RDVService(IRDVRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RDV>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<RDV> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(RDV rdv) => await _repository.AddAsync(rdv);
        public async Task UpdateAsync(RDV rdv) => await _repository.UpdateAsync(rdv);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
