using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortalGrupoAlyne.Model
{
    public class Etiqueta
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "inserir maximo de 100 caracteres")]
        public string? Titulo { get; set; }

        [StringLength(300, ErrorMessage = "inserir maximo de 100 caracteres")]
        public string? NomeTxt { get; set; }
        public string? Sql { get; set; }
        public string? printerAddress { get; set; }
        public int? Colunas { get; set; }
        public IEnumerable<EtiqParam>? Parametros { get; set; }

    }
}
