namespace OxenteBurguer.DTOs {
    public class PedidoCreateDto {
        public string NomeCliente { get; set; } = string.Empty;
        public List<int> IdsProdutos { get; set; } = new List<int>();
    }
}