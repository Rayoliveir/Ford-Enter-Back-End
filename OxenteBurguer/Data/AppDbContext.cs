using Microsoft.EntityFrameworkCore;
using OxenteBurguer.Models;

namespace OxenteBurguer.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}