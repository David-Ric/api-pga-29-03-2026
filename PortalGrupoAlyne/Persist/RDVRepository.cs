using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Persist;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortalGrupoAlyne.Data;
using PortalGrupoAlyne.Interfaces;

namespace PortalGrupoAlyne.Persistence
{
    public class RDVRepository : IRDVRepository
    {
        private readonly AppDbContext _context;

        public RDVRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RDV>> GetAllAsync() => await _context.Rdvs.ToListAsync();
        public async Task<RDV> GetByIdAsync(int id) => await _context.Rdvs.FindAsync(id);

        public async Task AddAsync(RDV rdv)
        {
            await _context.Rdvs.AddAsync(rdv);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RDV rdv)
        {
            _context.Rdvs.Update(rdv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rdv = await _context.Rdvs.FindAsync(id);
            if (rdv != null)
            {
                _context.Rdvs.Remove(rdv);
                await _context.SaveChangesAsync();
            }
        }
    }
}
