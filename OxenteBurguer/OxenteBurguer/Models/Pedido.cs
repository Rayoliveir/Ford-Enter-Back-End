using OxenteBurguer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxenteBurguer.Models {
    public class Pedido {
        public int NumeroPedido { get; set; }
        public string NomeCliente { get; set; }
        public List<Produto> Itens { get; set; } = new List<Produto>();
        public StatusPedido Status { get; set; }


        public decimal CalcularTotal() {
            decimal total = 0;
            foreach (var item in Itens) {
                total += item.Preco;
            }
            return total;
        }

    }
}
