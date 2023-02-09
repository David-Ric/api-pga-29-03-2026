using PortalGrupoAlyne.Model.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PortalGrupoAlyne.Model
{
   
    public class Produto 
    {
        [Key]
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? GrupoId { get; set; }
        public string? NomeGrupo { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
