using Microsoft.AspNetCore.Mvc;
using OxenteBurguer.Data;
using OxenteBurguer.Models;
using Microsoft.EntityFrameworkCore;

namespace OxenteBurguer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            // Retorna a lista de produtos do banco de dados
            return await _context.Produtos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // Retorna o status 201 (Created) e o link para o novo recurso
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
    }
}