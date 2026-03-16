using Microsoft.EntityFrameworkCore;
using PortalGrupoAlyne.Model;

namespace PortalGrupoAlyne.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RDV> Rdvs { get; set; }
    }
}
