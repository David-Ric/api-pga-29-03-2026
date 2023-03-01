using System.Xml.Serialization;

namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    [XmlRoot(ElementName = "serviceResponse")]
    public class PedidoVendaResponse
    {
        [XmlElement(ElementName = "serviceName")]
        public string serviceName { get; set; } = string.Empty;

        [XmlElement(ElementName = "status")]
        public string status { get; set; } = string.Empty;

        [XmlElement(ElementName = "pendingPrinting")]
        public string pendingPrinting { get; set; } = string.Empty;

        [XmlElement(ElementName = "transactionId")]
        public string transactionId { get; set; } = string.Empty;

        //[XmlElement(ElementName = "responseBody")]
        //public PedidoVendaResponseBody responseBody { get; set; }

        [XmlElement(ElementName = "statusMessage")]
        public string statusMessage { get; set; } = string.Empty;

        [XmlElement(ElementName = "message")]
        public string message { get; set; } = string.Empty;
    }

    /*
    [XmlType("responseBody")]
    public struct PedidoVendaResponseBody
    {
        [XmlArray("entities")]
        public Entities entities { get; set; } 
    }
    [XmlType("entities")]
    public struct Entities 
    {
        [XmlElement(ElementName = "total")]
        public string total { get; set; }
        [XmlElement(ElementName = "entity")]
        public Entity entity { get; set; } 
    }
    [XmlType("entity")]
    public struct Entity
    {
        [XmlElement(ElementName = "EMP")]
        public string? Filial { get; set; }
        [XmlElement(ElementName = "VEND")]
        public int VendedorId { get; set; }
        [XmlElement(ElementName = "PALMPV")]
        public string? PalmPV { get; set; }
        [XmlElement(ElementName = "COND")]
        public int TipoNegociacaoId { get; set; }
        [XmlElement(ElementName = "CLIENT")]
        public int ParceiroId { get; set; }
        [XmlElement(ElementName = "DATA")]
        public DateTime Data { get; set; }
        [XmlElement(ElementName = "VALOR")]
        public decimal? Valor { get; set; }
        [XmlElement(ElementName = "DTENTR")]
        public DateTime DataEntrega { get; set; }       
        [XmlElement(ElementName = "OBS")]
        public string? Observacao { get; set; }
    }
    */
}
