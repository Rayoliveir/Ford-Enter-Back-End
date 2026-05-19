using Microsoft.AspNetCore.Mvc;
using OxenteBurguer.Services;
using OxenteBurguer.DTOs;

namespace OxenteBurguer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public RelatoriosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("vendas-geral")]
        public async Task<ActionResult<RelatorioVendasDto>> GetRelatorioGeral()
        {
            var relatorio = await _pedidoService.GerarRelatorio();
            return Ok(relatorio);
        }
    }
}