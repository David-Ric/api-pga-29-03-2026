namespace PortalGrupoAlyne.Persist.Contratos
{
    public interface IVendedorPersist
    {
        Task<Vendedor> GetVendedoreTipoAsync(string tipo);
        Task<Vendedor> GetVendedorByIdAsync(int Id);
    }
}
