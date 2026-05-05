namespace OxenteBurguer.DTOs
{
    public class ItemPedidoDto
    {
        public int ProdutoId { get; set; }
        public string? PontoCarne { get; set; } // Opcional, só se for lanche
        public List<string>? Adicionais { get; set; }
    }
}