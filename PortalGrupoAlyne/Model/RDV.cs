using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalGrupoAlyne.Model
{
    public class RDV
    {
        public int Id { get; set; }  // Primary Key
        public string NomeCliente { get; set; }
        public string Observacao { get; set; }
        public string Municipio { get; set; }        
        public string UF { get; set; }
        public DateTime Data { get; set; }
        public string HoraIni { get; set; }
        public string HoraFin { get; set; }
        public int Objetivo { get; set; }  // Primary Key

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }
        public int km { get; set; }  // Primary Key

        /*
        public ObjetivoType Objetivo { get; set; }
        public enum ObjetivoType
        {
            Venda,
            Relacionamento,
            Prospecao,
            Negociacao,
            Treinamento,
            Reuniao,
            RCA,
            GA,
            VisitaPdv,
            HomeOffice
        }
        */
        
    }
}