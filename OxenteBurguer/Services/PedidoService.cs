using OxenteBurguer.Data;
using OxenteBurguer.Models;
using OxenteBurguer.Enums;
using Microsoft.EntityFrameworkCore;

namespace OxenteBurguer.Services {
    public class PedidoService {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context) {
            _context = context;
        }

        public async Task<Pedido> CriarPedido(string nomeCliente, List<int> idsProdutos) {
            var produtos = await _context.Produtos
                .Where(p => idsProdutos.Contains(p.Id))
                .ToListAsync();

            var novoPedido = new Pedido {
                NomeCliente = nomeCliente,
                Itens = produtos,
                Status = StatusPedido.Recebido
            };

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();
            return novoPedido;
        }
    }
}