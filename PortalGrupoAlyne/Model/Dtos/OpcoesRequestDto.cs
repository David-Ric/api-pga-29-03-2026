namespace PortalGrupoAlyne.Model.Dtos
{
    public class OpcoesRequestDto
    {
        public IEnumerable<int> ColunaModuloIds { get; set; }
        public IEnumerable<OpcaoCampoDto> OpcoesDto { get; set; }
    }
}
