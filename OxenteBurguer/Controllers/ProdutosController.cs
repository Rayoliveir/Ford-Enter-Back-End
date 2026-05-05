using Microsoft.AspNetCore.Mvc;
using OxenteBurguer.Data;
using OxenteBurguer.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase {
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos() {
        return await _context.Produtos.ToListAsync();
    }
}