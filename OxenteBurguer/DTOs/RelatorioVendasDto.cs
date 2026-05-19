namespace OxenteBurguer.DTOs
{
	public class RelatorioVendasDto
	{
		public int TotalPedidos { get; set; }
		public decimal FaturamentoTotal { get; set; }
		public int PedidosCancelados { get; set; }
		public List<ProdutoVendidoDto> ProdutosMaisVendidos { get; set; } = new();
	}

	public class ProdutoVendidoDto
	{
		public string Nome { get; set; } = string.Empty;
		public int Quantidade { get; set; }
	}
}