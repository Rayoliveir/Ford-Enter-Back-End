using OxenteBurguer.Data;
using OxenteBurguer.Models;
using OxenteBurguer.Enums;
using OxenteBurguer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace OxenteBurguer.Services
{
    public class PedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CriarPedido(PedidoCreateDto dto)
        {
            // 1. Validar se a lista de itens não está nula ou vazia
            if (dto.Itens == null || !dto.Itens.Any())
            {
                throw new Exception("O pedido precisa ter pelo menos um item.");
            }

            var novoPedido = new Pedido
            {
                NomeCliente = dto.NomeCliente,
                Status = StatusPedido.Recebido,
                Itens = new List<Produto>()
            };

            foreach (var itemDto in dto.Itens)
            {
                // 2. Tentar buscar o produto no banco
                var produtoBase = await _context.Produtos.FindAsync(itemDto.ProdutoId);

                // 3. Se o produto não existir, lançamos um erro
                if (produtoBase == null)
                {
                    throw new Exception($"Produto com ID {itemDto.ProdutoId} não encontrado no cardápio.");
                }

                if (produtoBase is Lanche lancheBase)
                {
                    var lancheCustomizado = new Lanche
                    {
                        Nome = lancheBase.Nome,
                        Preco = lancheBase.Preco,
                        Categoria = lancheBase.Categoria,
                        PontoCarne = itemDto.PontoCarne ?? "Ao ponto",
                        Adicionais = itemDto.Adicionais ?? new List<string>()
                    };
                    novoPedido.Itens.Add(lancheCustomizado);
                }
                else
                {
                    novoPedido.Itens.Add(produtoBase);
                }
            }

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();
            return novoPedido;
        }

        public async Task<List<Pedido>> ListarTodos()
        {
            return await _context.Pedidos.Include(p => p.Itens).ToListAsync();
        }

        public async Task<List<Pedido>> ListarPorStatus(StatusPedido status)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public async Task<Pedido?> AtualizarStatus(int numeroPedido, StatusPedido novoStatus)
        {
            var pedido = await _context.Pedidos.FindAsync(numeroPedido);

            if (pedido == null) return null;

            pedido.Status = novoStatus;
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido?> BuscarPorId(int numeroPedido)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.NumeroPedido == numeroPedido);
        }

        public async Task<bool> CancelarPedido(int numeroPedido)
        {
            var pedido = await _context.Pedidos.FindAsync(numeroPedido);
            if (pedido == null) return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RelatorioVendasDto> GerarRelatorio()
        {
            var pedidos = await _context.Pedidos.Include(p => p.Itens).ToListAsync();

            var relatorio = new RelatorioVendasDto
            {
                TotalPedidos = pedidos.Count,
                FaturamentoTotal = pedidos.Sum(p => p.Total),
                // Aqui contamos quantos pedidos foram deletados ou se você tiver um status "Cancelado"
                PedidosCancelados = 0,

                // Lógica para pegar os produtos mais vendidos
                ProdutosMaisVendidos = pedidos
                    .SelectMany(p => p.Itens)
                    .GroupBy(i => i.Nome)
                    .Select(g => new ProdutoVendidoDto
                    {
                        Nome = g.Key,
                        Quantidade = g.Count()
                    })
                    .OrderByDescending(x => x.Quantidade)
                    .Take(5) // Top 5 mais vendidos
                    .ToList()
            };

            return relatorio;
        }
    }


}