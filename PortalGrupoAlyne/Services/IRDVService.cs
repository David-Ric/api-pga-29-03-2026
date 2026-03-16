using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PortalGrupoAlyne.Services
{
    public interface IRDVService
    {
        Task<IEnumerable<RDV>> GetAllAsync();
        Task<RDV> GetByIdAsync(int id);
        Task<RDV> AddAsync(RDV rdv);
        Task<RDV> CreateAsync(RDV rdv);
        Task<RDV> UpdateAsync(int id, RDV rdv);
        Task<bool> DeleteAsync(int id);

        
    }
 
}