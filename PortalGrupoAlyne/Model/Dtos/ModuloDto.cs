using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model.Dtos
{
    public class ModuloDto
    {
        [Key]
        public int Id { get; set; }
        public int? MenuAdminId { get; set; }
        public int? SubMenuAdminId { get; set; }
        public int? MenuId { get; set; }
        public string? Descricao { get; set; }
        public string? Tabela { get; set; }
        public bool? insert { get; set; }
        public bool? update { get; set; }
        public bool? delete { get; set; }
        public string? icone { get; set; }
        public string? filtro1 { get; set; }
        public string? filtro2 { get; set; }
        public string? filtro3 { get; set; }
        public IEnumerable<ColunaModulo>? ColunaModulo { get; set; }
        public IEnumerable<LigacaoTabela>? LigacaoTabela { get; set; }
    }
}
