namespace PortalGrupoAlyne.Model.Dtos.Sankhya
{
    public class PedidoVendaRequest
    {
        public CabecalhoPedidoVenda CabecalhoPedidoVenda { get; set; }
        public IList<ItemPedidoVenda> ItemPedidoVenda { get; set; }
    }

    

    public class ItemPedidoVenda
    {
        public int Id { get; set; }
        public string? Filial { get; set; }
        public int VendedorId { get; set; }
        public string? PalMPV { get; set; }
        public int ProdutoId { get; set; }
        public decimal Quant { get; set; }
        public decimal ValUnit { get; set; }
        public decimal ValTotal { get; set; }
        public string? Baixado { get; set; }
    }
    public class CabecalhoPedidoVenda
    {
        public int Id { get; set; }
        public string? Filial { get; set; }
        public string? Lote { get; set; }
        public int VendedorId { get; set; }
        public string? PalmPV { get; set; }
        public int TipoNegociacaoId { get; set; }
        public int ParceiroId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEntrega { get; set; }
        public string? Observacao { get; set; }
        public string? Baixado { get; set; }
        public string? pedido { get; set; }
        public string? Status { get; set; }
    }

}
