using PortalGrupoAlyne.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalGrupoAlyne.Interfaces
{
    public interface IRDVRepository
    {
        Task<IEnumerable<RDV>> GetAllAsync();
        Task<RDV> GetByIdAsync(int id);
        Task AddAsync(RDV rdv);
        Task UpdateAsync(RDV rdv);
        Task DeleteAsync(int id);
    }
}
