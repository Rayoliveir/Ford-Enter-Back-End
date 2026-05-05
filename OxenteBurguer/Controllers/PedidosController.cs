using Microsoft.AspNetCore.Mvc;
using OxenteBurguer.DTOs;
using OxenteBurguer.Models;
using OxenteBurguer.Services;
using OxenteBurguer.Enums;

namespace OxenteBurguer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoCreateDto pedidoDto)
        {
            try
            {

                var pedido = await _pedidoService.CriarPedido(pedidoDto);
                return CreatedAtAction(nameof(GetPedidos), new { id = pedido.NumeroPedido }, pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoService.ListarTodos();
            return Ok(pedidos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPorStatus(StatusPedido status)
        {
            var pedidos = await _pedidoService.ListarPorStatus(status);
            return Ok(pedidos);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Pedido>> PatchStatus(int id, [FromBody] StatusPedido novoStatus)
        {
            var pedido = await _pedidoService.AtualizarStatus(id, novoStatus);

            if (pedido == null)
            {
                return NotFound($"Pedido nº {id} não encontrado.");
            }

            return Ok(pedido);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.BuscarPorId(id);
            if (pedido == null) return NotFound("Pedido não encontrado.");
            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var sucesso = await _pedidoService.CancelarPedido(id);
            if (!sucesso) return NotFound("Não foi possível cancelar: pedido inexistente.");

            return NoContent(); 
        }
    }
}