using OxenteBurguer.Enums;
using System.ComponentModel.DataAnnotations;

namespace OxenteBurguer.Models
{
    public class Pedido
    {
        [Key]
        public int NumeroPedido { get; set; }
        
        public string NomeCliente { get; set; } = string.Empty;
        public List<Produto> Itens { get; set; } = new List<Produto>();
        public StatusPedido Status { get; set; }

        public decimal Total => Itens?.Sum(item => item.Preco) ?? 0;
    }
}